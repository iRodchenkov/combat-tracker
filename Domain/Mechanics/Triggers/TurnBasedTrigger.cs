using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Mechanics.Triggers
{
    public abstract class TurnBasedTrigger: ITrigger
    {
        private readonly Guid _relatedTo;
        private readonly int _turnNumber;

        protected TurnBasedTrigger(Guid relatedTo, int turnNumber)
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
