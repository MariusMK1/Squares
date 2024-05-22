namespace Squares.Dtos;
public sealed class PointDto
{
    public int Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
    public override bool Equals(object? obj)
    {
        if (obj is null || GetType() != obj.GetType())
            return false;

        PointDto other = (PointDto)obj;
        return X == other.X && Y == other.Y;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}
