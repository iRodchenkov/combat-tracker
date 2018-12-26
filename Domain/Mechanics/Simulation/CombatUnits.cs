using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Domain.Units;

namespace Domain.Mechanics.Simulation
{
    [Serializable]
    internal sealed class CombatUnits: ICombatUnits
    {
        private readonly LinkedList<Unit> _units;

        [NonSerialized]
        private LinkedListNode<Unit> _currentNode;

        public CombatUnits(IEnumerable<Unit> units)
        {
            _units = new LinkedList<Unit>(units);
        }

        public Unit Current { get; private set; }

        public Unit MoveToFirstUnit()
        {
            SetCurrentUnit(_units.First);
            return Current;
        }

        public Unit MoveToNextUnit()
        {
            if (_currentNode == null && Current != null) _currentNode = _units.Find(Current);

            SetCurrentUnit(_currentNode?.Next);
            return Current;
        }

        public IEnumerator<Unit> GetEnumerator()
        {
            return _units.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _units).GetEnumerator();
        }

        private void SetCurrentUnit(LinkedListNode<Unit> node)
        {
            _currentNode = node;
            Current = node?.Value;
        }
    }

    public interface ICombatActivation
    {
        Unit Unit { get; }

        decimal Initiative { get; }
    }

    internal sealed class CombatActivation : ICombatActivation
    {
        public Unit Unit { get; }

        public decimal Initiative { get; set; }
    }
}
