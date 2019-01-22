using System;
using Domain.Conditions;
using Domain.Units;

namespace Domain.Mechanics.State
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
