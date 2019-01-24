using System;
using System.Collections.Generic;
using CombatTracker.Domain.Conditions;
using CombatTracker.Domain.Units;

namespace CombatTracker.Domain.Mechanics.State
{
    [Serializable]
    public sealed class CombatState
    {
        private readonly CombatActivations _activations;

        public CombatState()
        {
            _activations = new CombatActivations();
            EffectsToActivete = new List<Effect<IActiveCondition>>();
            EffectsToRemove = new List<Effect<ICondition>>();
            Round = 0;
            Phase = TurnPhases.BeginingOfCombat;
        }

        //public CombatState(IEnumerable<Unit> units, int round)
        //{
        //    if (units == null) throw new ArgumentNullException(nameof(units));
        //    if (round < 0) throw new ArgumentException(nameof(round));

        //    _activations = new CombatActivations(units);
        //    Round = round;
        //}

        public ICombatActivations Activations => _activations;

        public int Round { get; private set; }

        public TurnPhases Phase { get; set; }

        public List<Effect<ICondition>> EffectsToRemove { get; }

        public List<Effect<IActiveCondition>> EffectsToActivete { get; }

        public ICombatActivation AddActivation(Unit unit, decimal initiative)
        {
            return _activations.Add(unit, initiative);
        }

        public (int, ICombatActivation) StartNewRound()
        {
            _activations.MoveToFirActivation();
            return (++Round, _activations.Current);
        }

        public ICombatActivation NextActivation()
        {
            return _activations.MoveToNextActivation();
        }
    }
}
