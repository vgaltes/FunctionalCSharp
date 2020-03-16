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

        public double Distance
        {
            get => distance.GetValueOrDefault();
            set => distance = value;
        }

        Point(double x, double y) => (this.x, this.y, distance) = (x, y, default);

        public static bool operator ==(Point left, Point right) => (left.X, left.Y) == (right.X, right.Y);

        public static bool operator !=(Point left, Point right) => (left.X, left.Y) != (right.X, right.Y);

        public void SwapCoords() => (X, Y) = (Y, X);

        // First it evaluates the expressions in the right hand side, then the expressions in the left hand side and then it makes the assignments


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
