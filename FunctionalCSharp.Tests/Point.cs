using System;

namespace FunctionalCSharp.Tests
{
    public readonly struct Point
    {
        public double X
        {
            get;
        }

        public double Y
        {
            get;
        }

        public double Distance
        {
            get;
        }

        Point(double x, double y) => (X, Y, Distance) = (x, y, Math.Sqrt(x*x + y*y));

        public static bool operator ==(Point left, Point right) => (left.X, left.Y) == (right.X, right.Y);

        public static bool operator !=(Point left, Point right) => (left.X, left.Y) != (right.X, right.Y);


        public override bool Equals(object? obj)
        {
            return obj is Point other ? this == other : false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
    }
}
