﻿using CombatTracker.Domain.Conditions;
using CombatTracker.Domain.Mechanics;

namespace CombatTracker.DnD4e.Conditions
{
    public class Resistance : IResistanceCondition
    {
        public Resistance(string name, string description, ITrigger removeTrigger, DamageType damageType, int amount)
        {
            Name = name;
            Description = description;
            RemoveTrigger = removeTrigger;
            DamageType = damageType;
            Amount = amount;
        }

        public string Name { get; }

        public string Description { get; }

        public ITrigger RemoveTrigger { get; }

        public DamageType DamageType { get; }

        public int Amount { get; }
    }
}
