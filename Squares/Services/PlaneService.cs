using LiteDB;
using Squares.Dtos;
using Squares.LiteDb;
using Squares.Mappings;
using Squares.Models;

namespace Squares.Services;

public sealed class PlaneService(ILiteDbContext liteDbContext) : IPlaneService
{
    public readonly ILiteDatabase _liteDb = liteDbContext.Database;
    public List<Point> GetPoints()
    {
        var col = _liteDb.GetCollection<PointDto>("Plane");
        var dtos = col.FindAll();
        return PointMapping.Map(dtos);
    }
    public void Create(List<Point> points)
    {
        var col = _liteDb.GetCollection<PointDto>("Plane");
        col.DeleteAll();
        col.Insert(PointMapping.Map(points));
    }
    public int Insert(List<Point> points)
    {
        var col = _liteDb.GetCollection<PointDto>("Plane");
        var dtos = PointMapping.Map(points);
        foreach (var pointDto in dtos)
        {
            var exists = col.Find(p => p.X == pointDto.X && p.Y == pointDto.Y);
            if (exists.Any())
                throw new Exception($"Point already exists: {pointDto}");
        }
        return col.Insert(dtos);
    }
    public bool Delete(Point point)
    {
        var col = _liteDb.GetCollection<PointDto>("Plane");

        var pointsToDelete = col.Find(p => p.X == point.X && p.Y == point.Y);

        if (pointsToDelete.Any())
        {
            foreach (var pointToDelete in pointsToDelete)
            {
                col.Delete(pointToDelete.Id);
            }
            return true;
        }
        throw new Exception($"Point does not exist: {point}");
    }
}