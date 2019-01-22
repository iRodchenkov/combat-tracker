using System;
using Domain.Conditions;
using Domain.Mechanics;
using Domain.Mechanics.Simulation;
using Domain.Mechanics.State;
using Domain.Mechanics.Triggers;
using Domain.Units;
using Xunit;

namespace NetCoreTests
{
    public class CombatSimulationTest
    {
        [Fact]
        public void TestSimulation()
        {
            var state = new CombatState();
            var defini = GetDefini();
            state.AddActivation(defini, 5);
            var gautak = GetGautak();
            state.AddActivation(gautak, 10);
            
            var context = new ActionContext(new DamageCalculator());

            var simulation = new CombatSimulation(context);

            Assert.Null(state.Activations.Current);
            Assert.Equal(0, state.Round);
            Assert.Equal(TurnPhases.BeginingOfCombat, state.Phase);

            simulation.Next(state);
            Assert.NotNull(state.Activations.Current);
            Assert.Equal(gautak, state.Activations.Current.Unit);
            Assert.Equal(1, state.Round);
            Assert.Equal(TurnPhases.Turn, state.Phase);

            simulation.Next(state);
            Assert.Equal(defini, state.Activations.Current.Unit);
            Assert.Equal(1, state.Round);
            Assert.Equal(TurnPhases.Turn, state.Phase);

            simulation.Next(state);
            Assert.Equal(gautak, state.Activations.Current.Unit);
            Assert.Equal(2, state.Round);
            Assert.Equal(TurnPhases.Turn, state.Phase);

            Assert.Empty(defini.Conditions);
            PutOngoingDamageOn(defini);
            Assert.NotEmpty(defini.Conditions);

            simulation.Next(state);
            Assert.Equal(defini, state.Activations.Current.Unit);
            Assert.Equal(2, state.Round);
            Assert.Equal(TurnPhases.BeginingOfTurn, state.Phase);
            Assert.NotEmpty(defini.Conditions);
            Assert.NotEmpty(state.EffectsToActivete);

            simulation.Next(state)
                .OnSuccess(() => throw new Exception())
                .OnError(error =>
                {
                    Assert.Equal(CombatSimulationErrors.HasEffectsToApply, error);
                });
            Assert.Equal(defini, state.Activations.Current.Unit);
            Assert.Equal(2, state.Round);
            Assert.Equal(TurnPhases.BeginingOfTurn, state.Phase);
            Assert.NotEmpty(defini.Conditions);
            Assert.NotEmpty(state.EffectsToActivete);

            state.EffectsToActivete.Clear();

            simulation.Next(state);
            Assert.Equal(defini, state.Activations.Current.Unit);
            Assert.Equal(2, state.Round);
            Assert.Equal(TurnPhases.Turn, state.Phase);

            simulation.Next(state);
            Assert.Equal(defini, state.Activations.Current.Unit);
            Assert.Equal(2, state.Round);
            Assert.Equal(TurnPhases.SavingThrows, state.Phase);
            Assert.NotEmpty(defini.Conditions);
            Assert.Empty(state.EffectsToActivete);
            Assert.NotEmpty(state.EffectsToRemove);

            simulation.Next(state)
                .OnSuccess(() => throw new Exception())
                .OnError(error =>
                {
                    Assert.Equal(CombatSimulationErrors.HasEffectsToRemove, error);
                });
            Assert.Equal(defini, state.Activations.Current.Unit);
            Assert.Equal(2, state.Round);
            Assert.Equal(TurnPhases.SavingThrows, state.Phase);
            Assert.NotEmpty(defini.Conditions);
            Assert.Empty(state.EffectsToActivete);
            Assert.NotEmpty(state.EffectsToRemove);

            state.EffectsToRemove.Clear();

            simulation.Next(state);
            Assert.Equal(gautak, state.Activations.Current.Unit);
            Assert.Equal(3, state.Round);
            Assert.Equal(TurnPhases.Turn, state.Phase);
        }

        private const string Gautak = "Gautak", Defini = "Defini";

        private static Unit GetGautak() => new Unit(Guid.NewGuid(), Gautak, 80, 100, 0, null);
        private static Unit GetDefini() => new Unit(Guid.NewGuid(), Defini, 40, 50, 0, null);

        private static void PutOngoingDamageOn(Unit target)
        {
            target.AddConditions(new PeriodicDamage(5, DamageType.Fire, new BeginingOfTurnTrigger("", target), new SavingThrowTrigger("", target)));
        }
    }
}
