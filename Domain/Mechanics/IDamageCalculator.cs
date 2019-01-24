using System.Diagnostics.Contracts;

namespace CombatTracker.Domain.Mechanics
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
        [Pure]
        int ApplyModifiers(int amount, DamageType damageType, Conditions.Conditions conditions);
    }
}
