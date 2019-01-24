using System;
using CombatTracker.Domain.Mechanics.State;
using CombatTracker.Domain.Units;

namespace CombatTracker.Domain.Mechanics.Triggers
{
    [Serializable]
    public sealed class BeginingOfTurnTrigger: ITrigger
    {
        private readonly Unit _relatedTo;

        public BeginingOfTurnTrigger(string description, Unit relatedTo)
        {
            Description = description;
            _relatedTo = relatedTo;
        }

        public string Description { get; }

        public bool IsTriggered(CombatState state)
        {
            return state.Phase == TurnPhases.BeginingOfTurn
                && _relatedTo == state.Activations.Current.Unit;
        }
    }
}