using System;
using System.Collections.Generic;
using System.Text;
using Domain.Units;

namespace Domain.Mechanics
{
    public interface IDamageCalculator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="damageType"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int ApplyModifiers(int amount, DamageType damageType, Conditions.Conditions conditions);
    }
}
