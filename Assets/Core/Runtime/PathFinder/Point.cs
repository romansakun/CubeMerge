using System;

namespace Core.Runtime.PathFinder
{
    public struct Point
    {
        public readonly int X;
        public readonly int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(float x, float y)
        {
            X = (int)x;
            Y = (int)y;
        }
        
        public int Max()
        {
            return X > Y ? X : Y;
        }

        public int Min()
        {
            return X > Y ? Y : X;
        }

        public Point Abs()
        {
            return new Point(Math.Abs(X), Math.Abs(Y));
        }

        public static int SumDiff(Point a, Point b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }
        
        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }
        
        public static Point operator -(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }
        
        public static bool operator ==(Point a, Point b)
        {
            return a.X == b.X && a.Y == b.Y;
        }
        
        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }
    }
}