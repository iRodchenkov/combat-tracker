using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Conditions
{
    public sealed class Conditions : IEnumerable<ICondition>
    {
        private readonly ICondition[] _conditions;

        public Conditions(IEnumerable<ICondition> source)
        {
            _conditions = source?.ToArray() ?? new ICondition[0];
        }

        public IEnumerator<ICondition> GetEnumerator()
        {
            return ((IEnumerable<ICondition>)_conditions).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<ICondition>)_conditions).GetEnumerator();
        }
    }
}