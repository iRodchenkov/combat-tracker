using System;
using CombatTracker.Domain.Mechanics.State;
using CombatTracker.Domain.Units;

namespace CombatTracker.Domain.Mechanics.Triggers
{
    [Serializable]
    public sealed class EndOfTurnTrigger: ITrigger
    {
        private readonly Unit _relatedTo;

        private bool _shouldTriggerNextTime;

        public EndOfTurnTrigger(string description, Unit relatedTo, bool shouldTriggerNextTime)
        {
            Description = description;
            _relatedTo = relatedTo;
            _shouldTriggerNextTime = shouldTriggerNextTime;
        }

        public string Description { get; }

        public bool IsTriggered(CombatState state)
        {
            return state.Phase == TurnPhases.EndOfTurn
                && _shouldTriggerNextTime
                && _relatedTo == state.Activations.Current.Unit;
        }

        public void OnTurnCompleted(Unit unit)
        {
            if (_shouldTriggerNextTime) return;
            _shouldTriggerNextTime = _relatedTo == unit;
        }
    }
}