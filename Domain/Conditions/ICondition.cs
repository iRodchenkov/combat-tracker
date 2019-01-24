using CombatTracker.Domain.Mechanics;

namespace CombatTracker.Domain.Conditions
{
    public interface ICondition
    {
        string Name { get; }
        
        string Description { get; }

        ITrigger RemoveTrigger { get; }
    }
}