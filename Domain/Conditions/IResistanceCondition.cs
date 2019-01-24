using CombatTracker.Domain.Mechanics;

namespace CombatTracker.Domain.Conditions
{
    public interface IResistanceCondition: ICondition
    {
        DamageType DamageType { get; }
    }
}
