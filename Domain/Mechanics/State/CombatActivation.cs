using System;
using Domain.Units;

namespace Domain.Mechanics.State
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