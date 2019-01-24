using System;
using CombatTracker.Domain.Mechanics.State;
using CombatTracker.Domain.Units;

namespace CombatTracker.Domain.Mechanics.Triggers
{
    [Serializable]
    public sealed class SavingThrowTrigger: ITrigger
    {
        private readonly Unit _target;

        public SavingThrowTrigger(string description, Unit target)
        {
            Description = description;
            _target = target;
        }

        public string Description { get; }

        public bool IsTriggered(CombatState state)
        {
            return state.Phase == TurnPhases.SavingThrows
                && _target == state.Activations.Current.Unit;
        }
    }
}
