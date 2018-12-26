using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Domain.Units;

namespace Domain.Mechanics.Simulation
{
    [Serializable]
    public sealed class CombatState
    {
        private readonly CombatUnits _units;

        public CombatState(IEnumerable<Unit> units, int round)
        {
            if (units == null) throw new ArgumentNullException(nameof(units));
            if (round < 0) throw new ArgumentException(nameof(round));

            _units = new CombatUnits(units);
            Round = round;
        }

        public ICombatUnits Units => _units;

        public int Round { get; private set; }

        public (int, Unit) StartNewRound()
        {
            _units.MoveToFirstUnit();
            return (++Round, _units.Current);
        }

        public Unit MoveToNextUnit()
        {
            return _units.MoveToNextUnit();
        }
    }
}
