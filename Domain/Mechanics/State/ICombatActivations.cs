using System.Collections.Generic;

namespace CombatTracker.Domain.Mechanics.State
{
    public interface ICombatActivations : IEnumerable<ICombatActivation>
    {
        ICombatActivation Current { get; }
    }
}