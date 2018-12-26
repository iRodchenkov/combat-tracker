using System.Collections.Generic;
using Domain.Units;

namespace Domain.Mechanics.Simulation
{
    public interface ICombatUnits : IEnumerable<Unit>
    {
        Unit Current { get; }
    }
}