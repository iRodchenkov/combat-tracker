namespace CombatTracker.Domain.Mechanics
{
    public interface IActionContext
    {
        IDamageCalculator DamageCalculator { get; }
    }
}