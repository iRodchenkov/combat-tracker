using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Mechanics
{
    public sealed class ActionContext : IActionContext
    {
        public ActionContext(IDamageCalculator damageCalculator)
        {
            DamageCalculator = damageCalculator;
        }

        public IDamageCalculator DamageCalculator { get; }
    }
}
