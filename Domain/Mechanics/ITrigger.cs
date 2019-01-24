using CombatTracker.Domain.Mechanics.State;

namespace CombatTracker.Domain.Mechanics
{
    public interface ITrigger
    {
        string Description { get; }

        bool IsTriggered(CombatState state);
    }
}
