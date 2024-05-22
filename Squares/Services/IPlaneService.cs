using Squares.Models;

namespace Squares.Services;

public interface IPlaneService
{
    List<Point> GetPoints();
    void Create(List<Point> points);
    int Insert(List<Point> points);
    bool Delete(Point point);
}
