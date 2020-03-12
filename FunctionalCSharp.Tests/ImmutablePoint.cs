using System;

namespace FunctionalCSharp.Tests
{
    public readonly struct ImmutablePoint
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

        ImmutablePoint(double x, double y) => (X, Y, Distance) = (x, y, Math.Sqrt(x * x + y * y));

        public static bool operator ==(ImmutablePoint left, ImmutablePoint right) => (left.X, left.Y) == (right.X, right.Y);

        public static bool operator !=(ImmutablePoint left, ImmutablePoint right) => (left.X, left.Y) != (right.X, right.Y);

        public override bool Equals(object? obj) => obj is ImmutablePoint other ? this == other : false;


    
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
    }
}