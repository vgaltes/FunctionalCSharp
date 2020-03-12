using System;

namespace FunctionalCSharp.Tests
{
    public struct Point
    {
        private double x;

        public double X
        {
            get => x;
            set => x = value;
        }

        private double y;

        public double Y
        {
            get => y;
            set => y = value;
        }

        private double? distance;

        Point(double x, double y)
        {
            this.x = x;
            this.y = y;
            distance = default;
        }

        public static bool operator ==(Point left, Point right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        public static bool operator !=(Point left, Point right)
        {
            return left.X != right.X || left.Y != right.Y;
        }

        public void SwapCoords()
        {
            var tmp = X;
            X = Y;
            Y = tmp;
        }

        // First it evaluates the expressions in the right hand size, then the expressions in the left hand side and then it makes the assignments


        public override bool Equals(object? obj)
        {
            if (obj is Point)
            {
                var other = (Point)obj;
                return this == other;
            }
            else
            {
                return false;
            }
        }


        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        
    }
}
