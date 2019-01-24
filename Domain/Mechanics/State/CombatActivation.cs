using System;
using CombatTracker.Domain.Units;

namespace CombatTracker.Domain.Mechanics.State
{
    [Serializable]
    internal sealed class CombatActivation : ICombatActivation
    {
        public CombatActivation(Unit unit, decimal initiative)
        {
            Unit = unit;
            Initiative = initiative;
        }

        public Unit Unit { get; }

        public decimal Initiative { get; set; }
    }
}