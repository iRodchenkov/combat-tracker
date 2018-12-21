using System;

namespace Domain.Mechanics.Triggers
{
    public sealed class BeginingOfTurnTrigger: ITrigger
    {
        private readonly Guid _relatedTo;

        public BeginingOfTurnTrigger(Guid relatedTo)
        {
            _relatedTo = relatedTo;
        }

        public bool IsTriggered(Guid relatedTo)
        {
            return _relatedTo == relatedTo;
        }
    }
}