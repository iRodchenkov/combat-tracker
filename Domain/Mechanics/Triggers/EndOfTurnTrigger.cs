using System;
using Domain.Units;

namespace Domain.Mechanics.Triggers
{
    [Serializable]
    public sealed class EndOfTurnTrigger: ITrigger
    {
        private readonly Unit _relatedTo;
        private readonly int _turnNumber;

        public EndOfTurnTrigger(Unit relatedTo, int turnNumber)
        {
            _relatedTo = relatedTo;
            _turnNumber = turnNumber;
        }

        public bool IsTriggered(Unit relatedTo, int turnNumber)
        {
            return _relatedTo == relatedTo && _turnNumber <= turnNumber;
        }
    }
}