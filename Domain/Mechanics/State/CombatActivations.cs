using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CombatTracker.Domain.Units;

namespace CombatTracker.Domain.Mechanics.State
{
    [Serializable]
    internal sealed class CombatActivations: ICombatActivations
    {
        private readonly LinkedList<CombatActivation> _activations;

        [NonSerialized]
        private LinkedListNode<CombatActivation> _currentNode;

        private CombatActivation _current;

        public CombatActivations()
        {
            _activations = new LinkedList<CombatActivation>();
        }

        //public CombatActivations(IEnumerable<Unit> units)
        //{
        //    _activations = new LinkedList<CombatActivation>(units.Select(unit => new CombatActivation(unit, 0)));
        //}

        public ICombatActivation Current => _current;

        public ICombatActivation Add(Unit unit, decimal initiative)
        {
            var node = _activations.Find(_activations.LastOrDefault(a => a.Initiative >= initiative));
            return node == null
                ? _activations.AddFirst(new CombatActivation(unit, initiative)).Value
                : _activations.AddAfter(node, new CombatActivation(unit, initiative)).Value;
        }

        //public ICombatActivation Add(Unit unit, ICombatActivation addAfter)
        //{
        //    var node = _activations.Find((CombatActivation)addAfter);

        //}

        public CombatActivation MoveToFirActivation()
        {
            SetCurrentUnit(_activations.First);
            return _current;
        }

        public CombatActivation MoveToNextActivation()
        {
            if (_currentNode == null && _current != null) _currentNode = _activations.Find(_current);

            SetCurrentUnit(_currentNode?.Next);
            return _current;
        }

        public IEnumerator<ICombatActivation> GetEnumerator()
        {
            return _activations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _activations).GetEnumerator();
        }

        private void SetCurrentUnit(LinkedListNode<CombatActivation> node)
        {
            _currentNode = node;
            _current = node?.Value;
        }
    }
}
