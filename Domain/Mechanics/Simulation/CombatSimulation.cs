using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Domain.Conditions;
using Domain.Mechanics.State;
using Domain.Units;
using CombatSimulationResult = Core.ActionResult<Domain.Mechanics.Simulation.CombatSimulationErrors>;

namespace Domain.Mechanics.Simulation
{
    public sealed class CombatSimulation
    {
        private readonly IActionContext _context;

        public CombatSimulation(IActionContext context)
        {
            _context = context;
        }

        public CombatSimulationResult Next(CombatState state)
        {
            switch (state.Phase)
            {
                case TurnPhases.BeginingOfTurn:
                    return MoveToTurn(state);
                case TurnPhases.Turn:
                    return MoveToEndOfTurn(state);
                case TurnPhases.EndOfTurn:
                    return MoveToSavingThrows(state);
                case TurnPhases.BeginingOfCombat:
                case TurnPhases.SavingThrows:
                    return MoveToBeginingOfTurn(state);
                case TurnPhases.EndOfCombat:
                    return Error(CombatSimulationErrors.SimulationIsCompleted);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private CombatSimulationResult MoveToBeginingOfTurn(CombatState state)
        {
            if (state.EffectsToRemove.Any()) return Error(CombatSimulationErrors.HasEffectsToRemove);

            if (state.Activations.Current != null) state.NextActivation();
            if (state.Activations.Current == null)
            {
                state.StartNewRound();
                if (state.Activations.Current == null) return Error(CombatSimulationErrors.NoActivations);
            }
            state.Phase = TurnPhases.BeginingOfTurn;
            RemoveEffectsOnCurrentPhase(state);
            state.EffectsToActivete.AddRange(GetEffectsToActivateOnCurrentPhase(state));
            if (!state.EffectsToActivete.Any()) return MoveToTurn(state);
            return Success();
        }

        private CombatSimulationResult MoveToTurn(CombatState state)
        {
            if (state.EffectsToActivete.Any()) return Error(CombatSimulationErrors.HasEffectsToApply);
            state.Phase = TurnPhases.Turn;
            return Success();
        }

        private CombatSimulationResult MoveToEndOfTurn(CombatState state)
        {
            state.Phase = TurnPhases.EndOfTurn;
            state.EffectsToActivete.AddRange(GetEffectsToActivateOnCurrentPhase(state));
            if (!state.EffectsToActivete.Any()) return MoveToSavingThrows(state);
            return Success();
        }

        private CombatSimulationResult MoveToSavingThrows(CombatState state)
        {
            if (state.EffectsToActivete.Any()) return Error(CombatSimulationErrors.HasEffectsToApply);
            state.Phase = TurnPhases.SavingThrows;
            state.EffectsToRemove.AddRange(GetEffectsToRemoveOnCurrentPhase(state));
            if (!state.EffectsToRemove.Any()) return MoveToBeginingOfTurn(state);
            return Success();
        }

        private static void RemoveEffectsOnCurrentPhase(CombatState state)
        {
            foreach (var effect in GetEffectsToRemoveOnCurrentPhase(state))
            {
                effect.Unit.RemoveConditions(effect.Conditions);
            }
        }

        [Pure]
        private static IEnumerable<Effect<ICondition>> GetEffectsToRemoveOnCurrentPhase(CombatState state)
        {
            return FilterEffects(state, (unit, combatState) =>
            {
                return unit.Conditions
                    .GroupBy(c => c.RemoveTrigger)
                    .Where(t => t.Key != null && t.Key.IsTriggered(combatState))
                    .Select(x => new Effect<ICondition>
                    {
                        Unit = unit,
                        Trigger = x.Key,
                        Conditions = x.ToArray()
                    });
            });
        }

        [Pure]
        private static IEnumerable<Effect<IActiveCondition>> GetEffectsToActivateOnCurrentPhase(CombatState state)
        {
            return FilterEffects(state, (unit, combatState) =>
            {
                return unit.Conditions
                    .OfType<IActiveCondition>()
                    .GroupBy(c => c.ActivationTrigger)
                    .Where(t => t.Key != null && t.Key.IsTriggered(combatState))
                    .Select(x => new Effect<IActiveCondition>
                    {
                        Unit = unit,
                        Trigger = x.Key,
                        Conditions = x.ToArray()
                    });
            });
        }

        [Pure]
        private static IEnumerable<Effect<TCondition>> FilterEffects<TCondition>(CombatState state, Func<Unit, CombatState, IEnumerable<Effect<TCondition>>> selector)
            where TCondition : ICondition
        {
            return state.Activations.Select(x => x.Unit).Distinct().SelectMany(x => selector(x, state));
        }

        [Pure]
        private static CombatSimulationResult Success() => new CombatSimulationResult();

        [Pure]
        private static CombatSimulationResult Error(CombatSimulationErrors error) => new CombatSimulationResult(error);
    }

    public enum CombatSimulationErrors
    {
        NoActivations,
        HasEffectsToApply,
        HasEffectsToRemove,
        SimulationIsCompleted
    }
}
