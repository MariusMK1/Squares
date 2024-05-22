namespace Squares.Models;

public sealed class Point(int x, int y)
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
    public override bool Equals(object? obj)
    {
        if (obj is null || GetType() != obj.GetType())
            return false;
        Point other = (Point)obj;
        return X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + X.GetHashCode();
            hash = hash * 23 + Y.GetHashCode();
            return hash;
        }
    }
    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}
