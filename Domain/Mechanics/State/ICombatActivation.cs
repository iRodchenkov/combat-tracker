using CombatTracker.Domain.Units;

namespace CombatTracker.Domain.Mechanics.State
{
    public interface ICombatActivation
    {
        Unit Unit { get; }

        decimal Initiative { get; }
    }
}