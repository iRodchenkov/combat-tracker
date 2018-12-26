using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Domain.Mechanics.Simulation;
using Domain.Units;
using Xunit;

namespace NetCoreTests
{
    public class CombatStateTest
    {
        private const string Gautak = "Gautak", Defini = "Defini";

        private static Unit GetGautak() => new Unit(Guid.NewGuid(), "Gautak", 80, 100, 0, null);
        private static Unit GetDefini() => new Unit(Guid.NewGuid(), "Defini", 40, 50, 0, null);

        [Fact]
        public void TestSerialization()
        {
            var state = new CombatState(new[] { GetGautak(), GetDefini() }, 0);
            var (round, unit) = state.StartNewRound();

            Assert.Equal(1, round);
            Assert.Equal(1, state.Round);
            Assert.Equal(Gautak, unit.Name);
            Assert.Equal(Gautak, state.Units.Current.Name);

            byte[] bytes;

            var formater = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formater.Serialize(stream, state);
                bytes = stream.ToArray();
            }

            using (var stream = new MemoryStream(bytes))
            {
                var restoredState = (CombatState)formater.Deserialize(stream);

                Assert.Equal(1, restoredState.Round);
                Assert.Equal(Gautak, restoredState.Units.Current.Name);

                var u = restoredState.MoveToNextUnit();
                Assert.Equal(Defini, u.Name);
                Assert.Equal(Defini, restoredState.Units.Current.Name);
            }
        }
    }
}
