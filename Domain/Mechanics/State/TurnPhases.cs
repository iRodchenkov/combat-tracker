using System;

namespace Domain.Mechanics.State
{
    [Serializable]
    public enum TurnPhases
    {
        BeginingOfTurn,
        Turn,
        EndOfTurn,
        SavingThrows
    }
}
