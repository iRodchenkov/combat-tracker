using System;
using System.Collections.Generic;
using System.Text;
using Domain.Units;

namespace Domain.Mechanics
{
    public class DamageCalculator: IDamageCalculator
    {
        public int ApplyModifiers(int amount, DamageType damageType, Conditions.Conditions conditions)
        {
            return amount;
        }
    }
}
