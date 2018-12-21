using System;
using System.Collections.Generic;
using System.Text;
using Domain.Mechanics;
using Domain.Mechanics.Triggers;
using Domain.Units;

namespace Domain.Conditions
{
    public sealed class OngoingDamage : IActiveCondition
    {
        private readonly int _amount;
        private readonly DamageType _damageType;

        public OngoingDamage(Unit target, int amount, DamageType damageType)
        {
            _amount = amount;
            _damageType = damageType;
            Name = "Продолжительный урон";
            Description = $"В начале хода персонаж получит {amount} урона {damageType}";
            RemoveTrigger = new SavingThrowTrigger();
            ActivationTrigger = new BeginingOfTurnTrigger(target.Id);
        }

        public string Name { get; }

        public string Description { get; }

        public ITrigger RemoveTrigger { get; }

        public ITrigger ActivationTrigger { get; }

        public void Activate(Unit target, ActionContext context)
        {
            target.Damage(_amount, _damageType, context.DamageCalculator);
        }
    }
}
