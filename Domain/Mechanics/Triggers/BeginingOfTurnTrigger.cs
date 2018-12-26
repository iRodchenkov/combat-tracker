using System;
using Domain.Units;

namespace Domain.Mechanics.Triggers
{
    [Serializable]
    public sealed class BeginingOfTurnTrigger: ITrigger
    {
        private readonly Unit _relatedTo;

        public BeginingOfTurnTrigger(Unit relatedTo)
        {
            _relatedTo = relatedTo;
        }

        public bool IsTriggered(Unit relatedTo)
        {
            return _relatedTo == relatedTo;
        }
    }
}