using Squares.Dtos;
using Squares.Models;

namespace Squares.Mappings;

public static class PointMapping
{
    public static Point Map(PointDto pointDto)
    {
        return new Point (pointDto.X, pointDto.Y );
    }
    public static PointDto Map(Point point)
    {
        return new PointDto
        {
            X = point.X,
            Y = point.Y
        };
    }
    public static List<Point> Map(IEnumerable<PointDto> pointDtos)
    {
        return pointDtos.Select(Map).ToList();
    }
    public static List<PointDto> Map(IEnumerable<Point> points)
    {
        return points.Select(Map).ToList();
    }
}
