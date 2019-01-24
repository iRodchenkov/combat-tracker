namespace CombatTracker.Domain.Mechanics
{
    public sealed class ActionContext : IActionContext
    {
        public ActionContext(IDamageCalculator damageCalculator)
        {
            DamageCalculator = damageCalculator;
        }

        public IDamageCalculator DamageCalculator { get; }
    }
}
