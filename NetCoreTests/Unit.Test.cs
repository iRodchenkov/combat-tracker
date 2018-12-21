using Domain.Units;
using System;
using Xunit;

namespace NetCoreTests
{
    public class UnitTest
    {
        private static Unit GetGautak() => new Unit(Guid.NewGuid(), "Gautak", 80, 100, 0, null);

        [Fact]
        public void InitTest()
        {
            var gautak = GetGautak();

            Assert.Equal(80, gautak.CurrentHits);
            Assert.Equal(0, gautak.TempHits);
            Assert.Equal(100, gautak.MaxHits);
            Assert.False(gautak.IsDead);
            Assert.False(gautak.IsInjured);
        }

        [Fact]
        public void DamageTest()
        {
            var gautak = GetGautak();

            gautak.Damage(5);

            Assert.False(gautak.IsDead);
            Assert.False(gautak.IsInjured);
            Assert.Equal(75, gautak.CurrentHits);
            Assert.Equal(0, gautak.TempHits);
            Assert.Equal(100, gautak.MaxHits);

            Assert.Throws<ArgumentException>(() =>
            {
                gautak.Damage(-5);
            });
        }

        [Fact]
        public void HealingTest()
        {
            var gautak = GetGautak();

            gautak.Heal(5);

            Assert.False(gautak.IsDead);
            Assert.False(gautak.IsInjured);
            Assert.Equal(85, gautak.CurrentHits);
            Assert.Equal(0, gautak.TempHits);
            Assert.Equal(100, gautak.MaxHits);

            gautak.Heal(100500);

            Assert.False(gautak.IsDead);
            Assert.False(gautak.IsInjured);
            Assert.Equal(100, gautak.CurrentHits);
            Assert.Equal(0, gautak.TempHits);
            Assert.Equal(100, gautak.MaxHits);

            Assert.Throws<ArgumentException>(() =>
            {
                gautak.Heal(-5);
            });
        }

        [Fact]
        public void TempHitsTest()
        {
            var gautak = GetGautak();

            gautak.AddTempHits(5);

            Assert.False(gautak.IsDead);
            Assert.False(gautak.IsInjured);
            Assert.Equal(80, gautak.CurrentHits);
            Assert.Equal(5, gautak.TempHits);
            Assert.Equal(100, gautak.MaxHits);

            gautak.AddTempHits(10);

            Assert.False(gautak.IsDead);
            Assert.False(gautak.IsInjured);
            Assert.Equal(80, gautak.CurrentHits);
            Assert.Equal(10, gautak.TempHits);
            Assert.Equal(100, gautak.MaxHits);

            gautak.AddTempHits(5);

            Assert.False(gautak.IsDead);
            Assert.False(gautak.IsInjured);
            Assert.Equal(80, gautak.CurrentHits);
            Assert.Equal(10, gautak.TempHits);
            Assert.Equal(100, gautak.MaxHits);

            gautak.Damage(5);

            Assert.False(gautak.IsDead);
            Assert.False(gautak.IsInjured);
            Assert.Equal(80, gautak.CurrentHits);
            Assert.Equal(5, gautak.TempHits);
            Assert.Equal(100, gautak.MaxHits);

            gautak.Damage(15);

            Assert.False(gautak.IsDead);
            Assert.False(gautak.IsInjured);
            Assert.Equal(70, gautak.CurrentHits);
            Assert.Equal(0, gautak.TempHits);
            Assert.Equal(100, gautak.MaxHits);

            Assert.Throws<ArgumentException>(() =>
            {
                gautak.AddTempHits(-5);
            });
        }

        [Fact]
        public void StateTest()
        {
            var gautak = GetGautak();

            Assert.False(gautak.IsDead);
            Assert.False(gautak.IsInjured);

            gautak.Damage(50);

            Assert.False(gautak.IsDead);
            Assert.True(gautak.IsInjured);

            gautak.Damage(50);

            Assert.True(gautak.IsDead);
            Assert.True(gautak.IsInjured);
        }
    }
}
