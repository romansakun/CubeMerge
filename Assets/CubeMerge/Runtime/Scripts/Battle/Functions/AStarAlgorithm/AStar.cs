using System;
using System.Collections.Generic;

namespace CubeMerge.Runtime.Scripts.Battle
{
    public class AStar
    {
        private readonly float _step;
        private readonly MapArea _map;
        private readonly Stack<PositionNode> _nodePool = new Stack<PositionNode>();
        private readonly Stack<PositionNode> _nodeStack = new Stack<PositionNode>();
        private readonly Stack<PositionNode> _filterStack = new Stack<PositionNode>();
        private readonly Stack<Position> _resultPath = new Stack<Position>();
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
            
            _resultPath.Clear();
            _nodeStack.Clear();

            if (start == target)
                return _resultPath;
            
            _nodeStack.Push(GetNodeFromPool(start, null));

            var done = false;
            while (!done)
            {
                var currentNode = _nodeStack.Pop();
                
                _filterStack.Clear();    
                foreach (var offset in _walkingAround)
                {
                    var position = currentNode.Position + (offset * _step);
                    if (Position.SqrDistance(position, target) < _step * _step)
                    {
                        done = true;
                        _resultPath.Push(position);
                        while(currentNode.Next != null)
                        {
                            _nodePool.Push(currentNode.Next);
                            _resultPath.Push(currentNode.Position);
                            currentNode = currentNode.Next;
                        }
                        break;
                    }

                    if (!_map.IsFreePosition(position)) // || _nodes.Find(n => Position.SqrDistance(n.Position, position) < _step * _step) != null) 
                        continue;

                    var positionNode = GetNodeFromPool(position, currentNode);
                    if (_filterStack.Count == 0)
                    {
                        _filterStack.Push(positionNode);
                    }
                    else
                    {
                        var best = _filterStack.Peek();
                        if (Position.CompareDistances(best.Position, target, position, target) == 1)
                            _filterStack.Push(positionNode);
                        else
                            _nodePool.Push(positionNode);
                    }
                }
                if (_filterStack.Count > 0)
                {
                    _nodeStack.Push(_filterStack.Pop());
                    while (_filterStack.Count > 0)
                        _nodePool.Push(_filterStack.Pop());
                }
            }

            return _resultPath;
        }

        private PositionNode GetNodeFromPool(Position position, PositionNode node)
        {
            return _nodePool.Count > 0 
                ? _nodePool.Pop().Init(position, node) 
                : new PositionNode().Init(position, node);
        }
    }

    public class PositionNode
    {
        public Position Position;
        public PositionNode Next;

        public PositionNode Init(Position p, PositionNode next)
        {
            Position = p;
            Next = next;
            return this;
        }
    }
}