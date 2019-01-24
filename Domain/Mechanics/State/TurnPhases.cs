using System;

namespace CombatTracker.Domain.Mechanics.State
{
    [Serializable]
    public enum TurnPhases
    {
        BeginingOfCombat,
        BeginingOfTurn,
        Turn,
        EndOfTurn,
        SavingThrows,
        EndOfCombat
    }
}
