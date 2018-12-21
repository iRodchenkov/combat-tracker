using Domain.Mechanics;

namespace Domain.Conditions
{
    public interface ICondition
    {
        string Name { get; }
        
        string Description { get; }

        ITrigger RemoveTrigger { get; }
    }
}