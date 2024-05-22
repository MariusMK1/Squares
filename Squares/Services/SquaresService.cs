using Squares.Mappings;
using Squares.Models;
using System.Net;

namespace Squares.Services;
public sealed class SquaresService(IPlaneService planeService) : ISquaresService
{
    readonly IPlaneService _planeService = planeService;
    public List<List<Point>> FindSquares()
    {
        var points = _planeService.GetPoints().ToList();
        List<List<Point>> squares = [];

        for (int i = 0; i < points.Count - 3; i++)
        {
            for (int j = i + 1; j < points.Count - 2; j++)
            {
                for (int k = j + 1; k < points.Count - 1; k++)
                {
                    for (int l = k + 1; l < points.Count; l++)
                    {
                        if (IsSquare(points[i], points[j], points[k], points[l]))
                        {
                            List<Point> square = new List<Point> { points[i], points[j], points[k], points[l] };
                            squares.Add(square);
                        }
                    }
                }
            }
        }
        return squares;
    }
    bool IsSquare(Point p1, Point p2, Point p3, Point p4)
    {
        int d2 = SquareDistance(p1, p2);
        int d3 = SquareDistance(p1, p3);
        int d4 = SquareDistance(p1, p4);

        return d2 > 0 && d2 == d3 && 2 * d2 == d4 && 2 * SquareDistance(p2, p4) == SquareDistance(p2, p3);
    }

    static int SquareDistance(Point p1, Point p2)
    {
        int dx = p1.X - p2.X;
        int dy = p1.Y - p2.Y;
        return dx * dx + dy * dy;
    }
}
