using System.Collections.Generic;
using Domain.Units;

namespace Domain.Mechanics.State
{
    public interface ICombatActivations : IEnumerable<ICombatActivation>
    {
        ICombatActivation Current { get; }
    }
}