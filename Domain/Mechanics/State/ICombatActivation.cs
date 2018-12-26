using Domain.Units;

namespace Domain.Mechanics.State
{
    public interface ICombatActivation
    {
        Unit Unit { get; }

        decimal Initiative { get; }
    }
}