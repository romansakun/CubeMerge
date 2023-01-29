using UnityEngine;

namespace CubeMerge.Runtime.Scripts.Battle
{
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

        public override bool Equals(object obj) => obj is Position position && this == position;

        public override int GetHashCode() => this.X.GetHashCode() ^ this.Y.GetHashCode() << 2;

        public static Position operator +(Position a, Position b) => new Position(a.X + b.X, a.Y + b.Y);

        public static Position operator -(Position a, Position b) => new Position(a.X - b.X, a.Y - b.Y);

        public static Position operator *(Position a, float b) => new Position(a.X * b, a.Y * b);

        public static Position operator /(Position a, float b) => new Position(a.X / b, a.Y / b);

        public static float SqrtDistance(Position a, Position b)
        {
            var num1 = a.X - b.X;
            var num2 = a.Y - b.Y;
            return num1 * num1 + num2 * num2;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}