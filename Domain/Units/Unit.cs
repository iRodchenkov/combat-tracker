using System;
using System.Collections.Generic;
using System.Linq;
using CombatTracker.Domain.Conditions;
using CombatTracker.Domain.Mechanics;

namespace CombatTracker.Domain.Units
{
    [Serializable]
    public sealed class Unit
    {
        public Unit(Guid id, string name, int currentHits, int maxHits, int tempHits, IEnumerable<ICondition> conditions)
        {
            Id = id;
            Name = name;
            CurrentHits = currentHits;
            MaxHits = maxHits;
            TempHits = tempHits;
            Conditions = new Conditions.Conditions(conditions);
        }

        public Guid Id { get; }
        public string Name { get; private set; }
        public int CurrentHits { get; private set; }
        public int MaxHits { get; private set; }
        public int TempHits { get; private set; }

        public Conditions.Conditions Conditions { get; private set; }

        public void Damage(int amount)
        {
            Damage(amount, DamageType.Untyped, null);

        }

        public void Damage(int amount, DamageType type, IDamageCalculator damageProvider)
        {
            if (amount < 0) throw new ArgumentException();

            if (damageProvider != null) amount = damageProvider.ApplyModifiers(amount, type, Conditions);

            if (amount < TempHits) TempHits -= amount;
            else
            {
                CurrentHits -= amount - TempHits;
                TempHits = 0;
            }

        }

        public void Heal(int amount)
        {
            if (amount < 0) throw new ArgumentException();

            if (CurrentHits <= 0) CurrentHits = 1;

            CurrentHits = Math.Min(MaxHits, CurrentHits + amount);
        }

        public void AddTempHits(int amount)
        {
            if (amount < 0) throw new ArgumentException();

            TempHits = Math.Max(TempHits, amount);
        }

        public bool IsInjured => CurrentHits <= MaxHits / 2;

        public bool IsDead => CurrentHits <= 0;

        public void AddConditions(params ICondition[] conditions)
        {
            Conditions = new Conditions.Conditions(Conditions.Union(conditions));
        }

        public void RemoveConditions(params ICondition[] conditions)
        {
            Conditions = new Conditions.Conditions(Conditions.Where(c => !conditions.Contains(c)));
        }
    }
}
