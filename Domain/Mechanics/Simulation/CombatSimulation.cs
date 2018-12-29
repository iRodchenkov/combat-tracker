using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Conditions;
using Domain.Mechanics.State;
using Domain.Mechanics.Triggers;
using Domain.Units;
using CombatSimulationResult = Core.OperationResult<Domain.Mechanics.State.CombatState, Domain.Mechanics.Simulation.CombatSimulationErrors>;

namespace Domain.Mechanics.Simulation
{
    public sealed class CombatSimulation
    {
        private IActionContext _context;

        public CombatSimulation(IActionContext context)
        {
            _context = context;
        }

        public CombatSimulationResult Next(CombatState state)
        {
            if (state.Activations.Current == null)
            {
                StartNewRound(state);
                return new CombatSimulationResult(state);
            }
            if (state.Activations.Current == null) return new CombatSimulationResult(CombatSimulationErrors.NoActivations);

            switch (state.Phase)
            {
                case TurnPhases.BeginingOfTurn:
                    break;
                case TurnPhases.Turn:
                    break;
                case TurnPhases.EndOfTurn:
                    break;
                case TurnPhases.SavingThrows:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new CombatSimulationResult(state);
        }

        private void StartNewRound(CombatState state)
        {
            state.StartNewRound();
            StartNewTurn(state);
        }

        private void StartNewTurn(CombatState state)
        {
            state.Phase = TurnPhases.BeginingOfTurn;
            
        }

        private static void CompleteEffects<TTrigger>(CombatState state)
        {
            foreach (var effect in FilterEffects(state, Selector))
            {
                effect.Unit.RemoveConditions(effect.Conditions);
            }

            IEnumerable<Effect<ICondition>> Selector(Unit unit, Unit current)
            {
                return unit.Conditions
                    .GroupBy(c => c.RemoveTrigger as BeginingOfTurnTrigger)
                    .Where(t => t.Key != null && t.Key.IsTriggered(current))
                    .Select(x => new Effect<ICondition>
                    {
                        Unit = unit,
                        Trigger = x.Key,
                        Conditions = x.ToArray()
                    });
            }
        }

        private static void CompleteEffectsAtTheBeginigOfTheTurn(CombatState state)
        {
            foreach (var effect in FilterEffects(state, Selector))
            {
                effect.Unit.RemoveConditions(effect.Conditions);
            }

            IEnumerable<Effect<ICondition>> Selector(Unit unit, Unit current)
            {
                return unit.Conditions
                    .GroupBy(c => c.RemoveTrigger as BeginingOfTurnTrigger)
                    .Where(t => t.Key != null && t.Key.IsTriggered(current))
                    .Select(x => new Effect<ICondition>
                    {
                        Unit = unit,
                        Trigger = x.Key,
                        Conditions = x.ToArray()
                    });
            }
        }

        private void GetEffectsWhichActivateAtTheBeginigOfTheTurn(CombatState state)
        {
            foreach (var effect in FilterEffects(state, Selector))
            {
                foreach (var condition in effect.Conditions)
                {
                    condition.Activate(effect.Unit, _context);
                }
            }

            IEnumerable<Effect<IActiveCondition>> Selector(Unit unit, Unit current)
            {
                return unit.Conditions.OfType<IActiveCondition>()
                    .GroupBy(c => c.ActivationTrigger as BeginingOfTurnTrigger)
                    .Where(t => t.Key != null && t.Key.IsTriggered(current))
                    .Select(x => new Effect<IActiveCondition>
                    {
                        Unit = unit,
                        Trigger = x.Key,
                        Conditions = x.ToArray()
                    });
            }
    }

        private static void CompleteEffectsAtTheEndOfTheTurn(CombatState state)
        {
            var effects = state.Activations.SelectMany(a => a.Unit.Conditions.Select(c => (unit: a.Unit, condition: c)))
                .Where(x => (x.condition.RemoveTrigger as EndOfTurnTrigger)?.IsTriggered(state.Activations.Current.Unit) ?? false);
        }

        private static void GetEffectsWhichActivateAtTheEndOfTheTurn(CombatState state)
        {
            var effects = state.Activations.SelectMany(a => a.Unit.Conditions.Select(c => (unit: a.Unit, condition: c)))
                .Where(x => ((x.condition as IActiveCondition)?.ActivationTrigger as EndOfTurnTrigger)?.IsTriggered(
                                state.Activations.Current.Unit) ?? false);
        }

        private static void GetEffectsWhichFinishWithTheSavingThrow(CombatState state)
        {
            var effects = state.Activations.SelectMany(a => a.Unit.Conditions.Select(c => (unit: a.Unit, condition: c)))
                .Where(x => x.condition.RemoveTrigger is SavingThrowTrigger);
        }

        private static void OnTurnCompleted(CombatState state)
        {
            var effects = state.Activations.SelectMany(a => a.Unit.Conditions.Select(c => (unit: a.Unit, condition: c)))
                .Select(x => x.condition.RemoveTrigger as EndOfTurnTrigger).Where(x => x != null).Distinct();

            foreach (var effect in effects)
            {
                effect.OnTurnCompleted(state.Activations.Current.Unit);
            }
        }

        private static IEnumerable<Effect<TCondition>> FilterEffects<TCondition>(CombatState state, Func<Unit, Unit, IEnumerable<Effect<TCondition>>> selector)
            where TCondition : ICondition
        {
            return state.Activations.Select(x => x.Unit).Distinct().SelectMany(x => selector(x, state.Activations.Current.Unit));
        }

        private sealed class Effect<TCondition>
            where TCondition: ICondition
        {
            public Unit Unit { get; set; }
            public ITrigger Trigger { get; set; }
            public TCondition [] Conditions { get; set; }
        }
    }

    public enum CombatSimulationErrors
    {
        NoActivations
    }
}
