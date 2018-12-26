using Domain.Units;
using System;
using Domain.Conditions;
using Domain.Mechanics;
using Domain.Mechanics.Triggers;
using Xunit;

namespace NetCoreTests
{
    public class ConditionsTest
    {
        private static Unit GetGautak() => new Unit(Guid.NewGuid(), "Gautak", 80, 100, 0, null);
        private static Unit GetDefini() => new Unit(Guid.NewGuid(), "Defini", 40, 50, 0, null);

        [Fact]
        public void OngoingDamageTest()
        {
            var gautak = GetGautak();
            var defini = GetDefini();
            var context = new ActionContext(new DamageCalculator());

            Assert.Empty(gautak.Conditions);

            gautak.AddConditions(new OngoingDamage(gautak, 10, DamageType.Fire & DamageType.Ice));

            Assert.Single(gautak.Conditions);

            foreach (var condition in gautak.Conditions)
            {
                if (condition is IActiveCondition active
                    && active.ActivationTrigger is BeginingOfTurnTrigger beginingOfTurnTrigger)
                {
                    Assert.False(beginingOfTurnTrigger.IsTriggered(defini));
                    Assert.True(beginingOfTurnTrigger.IsTriggered(gautak));

                    active.Activate(gautak, context);
                    Assert.Equal(70, gautak.CurrentHits);

                    active.Activate(gautak, context);
                    Assert.Equal(60, gautak.CurrentHits);

                    active.Activate(gautak, context);
                }
            }

            Assert.Equal(50, gautak.CurrentHits);
        }
    }
}
