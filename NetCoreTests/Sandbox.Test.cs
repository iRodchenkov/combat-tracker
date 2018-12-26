using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Xunit;

namespace NetCoreTests
{
    public class SandboxTest
    {
        [Serializable]
        private sealed class A
        {
            public int Value { get; set; }
        }

        [Serializable]
        private sealed class B
        {
            public string Value { get; set; }

            public A A { get; set; }
        }

        [Fact]
        public void TestSerialization()
        {
            A a1 = new A {Value = 1}, a2 = new A {Value = 2};
            B b1 = new B {Value = "1", A = a1}, b11 = new B {Value = "11", A = a1}, b2 = new B {Value = "2", A = a2};

            var array = new[] {b1, b11, b2, b2};

            byte[] bytes;

            var formater = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formater.Serialize(stream, array);
                bytes = stream.ToArray();
            }

            using (var stream = new MemoryStream(bytes))
            {
                var newArray = (B[])formater.Deserialize(stream);

                Assert.Equal(4, newArray.Length);

                Assert.True(newArray[0].A == newArray[1].A);
                Assert.False(newArray[2].A == newArray[1].A);
                Assert.False(newArray[0].A == newArray[2].A);
                Assert.True(newArray[2] == newArray[3]);
            }
        }
    }
}
