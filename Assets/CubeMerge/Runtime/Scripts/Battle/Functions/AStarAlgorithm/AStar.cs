using System;
using System.Collections.Generic;

namespace CubeMerge.Runtime.Scripts.Battle
{
    public class AStar
    {
        private readonly MapArea _map;
        private readonly float _step;
        private readonly List<PositionNode> _nodes = new List<PositionNode>();
        private readonly Stack<PositionNode> _nodeStack = new Stack<PositionNode>();
        private readonly Position[] _walkingAround = new Position[]
        {
            new Position(-0.7071f, 0.7071f),
            new Position(0.7071f, -0.7071f),
            new Position(-0.7071f, -0.7071f),
            new Position(0.7071f, 0.7071f),
            new Position(-0.9239f, 0.3827f),
            new Position(0.9239f, -0.3827f),
            new Position(-0.9239f, -0.3827f),
            new Position(0.9239f, 0.3827f),
            new Position(-0.3827f, 0.9239f),
            new Position(0.3827f, -0.9239f),
            new Position(-0.3827f, -0.9239f),
            new Position(0.3827f, 0.9239f),
            new Position(-1, 0),
            new Position(1, 0),
            new Position(0, -1),
            new Position(0, 1)
        };

        public AStar(MapArea map, float step)
        {
            _map = map;
            _step = step;
        }

        public Stack<Position> GetPath(Position start, Position target)
        {
            if (!_map.Bounds.IsInBounds(target) || !_map.Bounds.IsInBounds(start))
                throw new Exception("Start or target is not in map bounds!");

            var result = new Stack<Position>();
            if (start == target)
                return result;

            _nodes.Clear();
            _nodeStack.Clear();
            _nodeStack.Push(new PositionNode(){Position = start});
            
            var done = false;
            while (!done)
            {
                var currentNode = _nodeStack.Pop();
                
                var filterStack = new Stack<PositionNode>();    
                foreach (var offset in _walkingAround)
                {
                    var position = currentNode.Position + (offset * _step);
                    if (Position.SqrDistance(position, target) < _step * _step)
                    {
                        done = true;
                        result.Push(position);
                        while(currentNode.Next != null)
                        {
                            result.Push(currentNode.Position);
                            currentNode = currentNode.Next;
                        }
                        break;
                    }

                    if (!_map.IsFreePosition(position)) // || _nodes.Find(n => Position.SqrDistance(n.Position, position) < _step * _step) != null) 
                        continue;

                    var positionNode = new PositionNode
                    {
                        Position = position,
                        Next = currentNode
                    };
                    
                    if (filterStack.Count == 0)
                    {
                        filterStack.Push(positionNode);
                    }
                    else
                    {
                        var best = filterStack.Peek();
                        if (Position.CompareDistances(best.Position, target, position, target) == 1)
                            filterStack.Push(positionNode);
                    }
                }
                
                _nodes.Add(currentNode);
                if (filterStack.Count > 0)
                    _nodeStack.Push(filterStack.Pop());
            }

            return result;
        }
    }

    public class PositionNode
    {
        public Position Position;
        public PositionNode Next;
    }
}