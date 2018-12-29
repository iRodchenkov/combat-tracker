using System;
using Domain.Units;

namespace Domain.Mechanics.Triggers
{
    [Serializable]
    public sealed class EndOfTurnTrigger: ITrigger
    {
        private readonly Unit _relatedTo;

        private bool _shouldTriggerNextTime;

        public EndOfTurnTrigger(Unit relatedTo, bool shouldTriggerNextTime)
        {
            _relatedTo = relatedTo;
            _shouldTriggerNextTime = shouldTriggerNextTime;
        }

        public bool IsTriggered(Unit relatedTo)
        {
            return _shouldTriggerNextTime && _relatedTo == relatedTo;
        }

        public void OnTurnCompleted(Unit unit)
        {
            if (_shouldTriggerNextTime) return;
            _shouldTriggerNextTime = _relatedTo == unit;
        }
    }
}