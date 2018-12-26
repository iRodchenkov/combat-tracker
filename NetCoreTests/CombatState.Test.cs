using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Domain.Mechanics.State;
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
            var state = new CombatState();
            state.AddActivation(GetDefini(), 5);
            state.AddActivation(GetGautak(), 10);
            var (round, activation) = state.StartNewRound();

            Assert.Equal(1, round);
            Assert.Equal(1, state.Round);
            Assert.Equal(Gautak, activation.Unit.Name);
            Assert.Equal(Gautak, state.Activations.Current.Unit.Name);

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
                Assert.Equal(Gautak, restoredState.Activations.Current.Unit.Name);

                var a = restoredState.NextActivation();
                Assert.Equal(Defini, a.Unit.Name);
                Assert.Equal(Defini, restoredState.Activations.Current.Unit.Name);

                a = restoredState.NextActivation();
                Assert.Null(a);

                restoredState.StartNewRound();
                Assert.Equal(2, restoredState.Round);
                Assert.Equal(Gautak, restoredState.Activations.Current.Unit.Name);

                a = restoredState.NextActivation();
                Assert.Equal(2, restoredState.Round);
                Assert.Equal(Defini, a.Unit.Name);
                Assert.Equal(Defini, restoredState.Activations.Current.Unit.Name);

                a = restoredState.NextActivation();
                Assert.Null(a);

                restoredState.StartNewRound();
                Assert.Equal(3, restoredState.Round);
                Assert.Equal(Gautak, restoredState.Activations.Current.Unit.Name);
            }
        }
    }
}
