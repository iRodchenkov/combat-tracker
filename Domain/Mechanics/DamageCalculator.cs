namespace CombatTracker.Domain.Mechanics
{
    public class DamageCalculator: IDamageCalculator
    {
        public int ApplyModifiers(int amount, DamageType damageType, Conditions.Conditions conditions)
        {
            return amount;
        }
    }
}
