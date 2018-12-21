using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Mechanics
{
    public sealed class ActionContext
    {
        public ActionContext(IDamageCalculator damageCalculator)
        {
            DamageCalculator = damageCalculator;
        }

        public IDamageCalculator DamageCalculator { get; }
    }
}
