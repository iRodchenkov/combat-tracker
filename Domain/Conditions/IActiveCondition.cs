using CombatTracker.Domain.Mechanics;
using CombatTracker.Domain.Units;

namespace CombatTracker.Domain.Conditions
{
    public interface IActiveCondition: ICondition
    {
        void Activate(Unit target, IActionContext context);
        ITrigger ActivationTrigger { get; }
    }
}
