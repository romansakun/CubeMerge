using System;
using System.Collections.Generic;

namespace CubeMerge.Runtime.Scripts.Battle
{
    public class AStar
    {
        private readonly MapArea _map;
        private readonly Position[] _walkingAround = new Position[]
        {
            new Position(-1, 0),
            new Position(1, 0),
            new Position(0, -1),
            new Position(0, 1),
            new Position(1, -1),
            new Position(-1, 1),
            new Position(-1, -1),
            new Position(1, 1),
        };

        public AStar(MapArea map)
        {
            _map = map;
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
                
                foreach (var offset in _walkingAround)
                {
                    var position = currentNode.Position + offset;
                    if (position == target)
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
                    nodes.Add(currentNode);
                    nodeStack.Push(positionNode);
                }
            }
            
            return result;
        }
    }

    public class PositionNode
    {
        public Position Position;
        public PositionNode Next;
    }

    public class MapArea
    {
        public Bounds Bounds;
        public List<Bounds> Obstacles;

        public MapArea()
        {
            Bounds = new Bounds(new Position(0, 0), 8, 10);
            Obstacles = new List<Bounds>();
        }
        
        public bool IsFreePosition(Position position)
        {
            if (!Bounds.IsInBounds(position))
                return false;

            if (Obstacles == null || Obstacles.Count == 0)
                return true;
            
            foreach (var obstacle in Obstacles)
            {
                if (obstacle.IsInBounds(position))
                    return false;
            }
            return true;
        }
    }

    public struct Bounds
    {
        public Position Center;
        public float Width;
        public float Heigth;

        public Bounds(Position center, float width, float heigth)
        {
            Center = center;
            Width = width;
            Heigth = heigth;
        }

        public Bounds(float width, float heigth)
        {
            Center = new Position(0, 0);
            Width = width;
            Heigth = heigth;
        }

        public bool IsInBounds(Position p)
        {
            return p.X <= Center.X + Width / 2 && p.X >= Center.X - Width / 2 && 
                   p.Y <= Center.Y + Heigth / 2 && p.Y >= Center.Y - Heigth / 2;
        }
    }
    
    public struct Position
    {
        public float X;
        public float Y;

        public Position(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Position lhs, Position rhs)
        {
            var num1 = lhs.X - rhs.X;
            var num2 = lhs.Y - rhs.Y;
            return num1 * num1 + num2 * num2 < float.Epsilon;
        }

        public static bool operator !=(Position lhs, Position rhs)
        {
            return !(lhs == rhs);
        }
        
        public static Position operator +(Position a, Position b) => new Position(a.X + b.X, a.Y + b.Y);

        public static Position operator -(Position a, Position b) => new Position(a.X - b.X, a.Y - b.Y);

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}