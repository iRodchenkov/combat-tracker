using System;
using System.Collections.Generic;
using System.Text;
using Domain.Mechanics.State;

namespace Domain.Mechanics
{
    public interface ITrigger
    {
        string Description { get; }

        bool IsTriggered(CombatState state);
    }
}
