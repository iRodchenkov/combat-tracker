using System;
using System.Collections.Generic;
using Domain.Units;

namespace Domain.Mechanics.State
{
    [Serializable]
    public sealed class CombatState
    {
        private readonly CombatActivations _activations;

        public CombatState()
        {
            _activations = new CombatActivations();
            Round = 0;
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
