using System;
using CombatTracker.Domain.Conditions;
using CombatTracker.Domain.Units;

namespace CombatTracker.Domain.Mechanics.State
{
    [Serializable]
    public sealed class Effect<TCondition>
        where TCondition : ICondition
    {
        public Unit Unit { get; set; }
        public ITrigger Trigger { get; set; }
        public TCondition[] Conditions { get; set; }
    }
}
