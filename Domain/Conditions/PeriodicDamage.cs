using System;
using System.Collections.Generic;
using System.Text;
using Domain.Mechanics;
using Domain.Units;

namespace Domain.Conditions
{
    [Serializable]
    public sealed class PeriodicDamage : IActiveCondition
    {
        private readonly int _amount;
        private readonly DamageType _damageType;

        public PeriodicDamage(int amount, DamageType damageType, ITrigger activationTrigger, ITrigger removeTrigger)
        {
            _amount = amount;
            _damageType = damageType;
            Name = "Продолжительный урон";
            Description = $"В начале хода персонаж получит {amount} урона {damageType}";
            RemoveTrigger = removeTrigger;
            ActivationTrigger = activationTrigger;
        }

        public string Name { get; }

        public string Description { get; }

        public ITrigger RemoveTrigger { get; }

        public ITrigger ActivationTrigger { get; }

        public void Activate(Unit target, IActionContext context)
        {
            target.Damage(_amount, _damageType, context.DamageCalculator);
        }
    }
}
