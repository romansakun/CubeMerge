using System;
using System.Collections.Generic;

namespace CubeMerge.Runtime.Scripts.Battle
{
    public class AStar
    {
        private readonly MapArea _map;
        private readonly float _step;
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

            var nodes = new List<PositionNode>();
            var nodeStack = new Stack<PositionNode>();
            nodeStack.Push(new PositionNode(){Position = start});
            
            var done = false;
            while (!done)
            {
                var currentNode = nodeStack.Pop();
                
                var filterStack = new Stack<PositionNode>();    
                foreach (var offset in _walkingAround)
                {
                    var position = currentNode.Position + (offset * _step);
                    if (Position.SqrtDistance(position, target) < _step)
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

                    if (!_map.IsFreePosition(position) || nodes.Find(n => n.Position == position) != null) 
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
                        if (Position.SqrtDistance(best.Position, target) > Position.SqrtDistance(position, target))
                            filterStack.Push(positionNode);
                    }
                }
                
                nodes.Add(currentNode);
                if (filterStack.Count > 0)
                    nodeStack.Push(filterStack.Pop());
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