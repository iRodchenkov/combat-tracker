using System;

namespace Domain.Mechanics.Triggers
{
    public sealed class EndOfTurnTrigger: ITrigger
    {
        private readonly Guid _relatedTo;
        private readonly int _turnNumber;

        public EndOfTurnTrigger(Guid relatedTo, int turnNumber)
        {
            _relatedTo = relatedTo;
            _turnNumber = turnNumber;
        }

        public bool IsTriggered(Guid relatedTo, int turnNumber)
        {
            return _relatedTo == relatedTo && _turnNumber <= turnNumber;
        }
    }
}