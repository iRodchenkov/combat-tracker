//using System;
//using Domain.Mechanics;
//using Domain.Mechanics.Triggers;
//using Domain.Units;

//namespace Domain.Conditions
//{
//    [Serializable]
//    public sealed class OngoingDamage : IActiveCondition
//    {
//        private readonly int _amount;
//        private readonly DamageType _damageType;

//        public OngoingDamage(Unit target, int amount, DamageType damageType)
//        {
//            _amount = amount;
//            _damageType = damageType;
//            Name = "Продолжительный урон";
//            Description = $"В начале хода персонаж получит {amount} урона {damageType}";
//            RemoveTrigger = new SavingThrowTrigger("Продолжительный урон", target);
//            ActivationTrigger = new BeginingOfTurnTrigger("Продолжительный урон", target);
//        }

//        public string Name { get; }

//        public string Description { get; }

//        public ITrigger RemoveTrigger { get; }

//        public ITrigger ActivationTrigger { get; }

//        public void Activate(Unit target, IActionContext context)
//        {
//            target.Damage(_amount, _damageType, context.DamageCalculator);
//        }
//    }
//}
