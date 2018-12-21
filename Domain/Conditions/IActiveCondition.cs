using Domain.Mechanics;
using Domain.Units;

namespace Domain.Conditions
{
    public interface IActiveCondition: ICondition
    {
        void Activate(Unit target, ActionContext context);
        ITrigger ActivationTrigger { get; }
    }
}
