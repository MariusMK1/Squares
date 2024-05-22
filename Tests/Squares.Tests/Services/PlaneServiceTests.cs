using LiteDB;
using Squares.Dtos;
using Squares.LiteDb;
using Squares.Models;
using Squares.Services;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Squares.Tests.Services;
[ExcludeFromCodeCoverage]
public class PlaneServiceTests
{
    [Fact]
    public void GetPoints_ReturnsListOfPoints()
    {
        var cut = SetupCut();

        var result = cut.GetPoints();

        Assert.Equal(1, result[0].X);
        Assert.Equal(2, result[0].Y);
    }
    [Fact]
    public void Create_VerifyReceived()
    {
        var liteDatabase = Substitute.For<ILiteCollection<PointDto>>();
        var cut = SetupCut(liteDatabase);
        var points = new List<Point> { new(1, 2) };

        cut.Create(points);

        liteDatabase.Received().DeleteAll();
        liteDatabase.Received().Insert(Arg.Any<List<PointDto>>());
    }

    [Fact]
    public void Insert_VerifyReceived()
    {
        var liteDatabase = Substitute.For<ILiteCollection<PointDto>>();
        var cut = SetupCut(liteDatabase);
        var points = new List<Point> { new(1, 2) };

        cut.Insert(points);

        liteDatabase.Received().Insert(Arg.Any<List<PointDto>>());
    }
    [Fact]
    public void Insert_WhenAlreadyContainsTheSamePoint_Throws()
    {
        var liteDatabase = Substitute.For<ILiteCollection<PointDto>>();
        var cut = SetupCut(liteDatabase);
        var points = new List<Point> { new(1, 2) };
        liteDatabase.Find(Arg.Any<Expression<Func<PointDto, bool>>>(), 0, int.MaxValue).Returns(new List<PointDto> { new() { X = 1, Y = 2 } });
        var result = Assert.Throws<Exception>(() => cut.Insert(points));

        Assert.Equal("Point already exists: (1, 2)", result.Message);
    }
    [Fact]
    public void Delete_VerifyReceived()
    {
        var liteDatabase = Substitute.For<ILiteCollection<PointDto>>();
        var cut = SetupCut(liteDatabase);
        var point = new Point(1, 2);
        liteDatabase.Find(Arg.Any<Expression<Func<PointDto, bool>>>(), 0, int.MaxValue).Returns(new List<PointDto> { new() {Id = 3, X = 1, Y = 2 } });
        cut.Delete(point);

        liteDatabase.Received().Delete(3);
    }
    [Fact]
    public void Delete_WhenPointDoesNotExist_Throws()
    {
        var cut = SetupCut();
        var point = new Point(1, 2);
        var result = Assert.Throws<Exception>(() => cut.Delete(point));

        Assert.Equal("Point does not exist: (1, 2)", result.Message);
    }
    private static PlaneService SetupCut(ILiteCollection<PointDto>? liteCollection = null)
    {
        var liteDbContext = Substitute.For<ILiteDbContext>();
        var liteDatabase = Substitute.For<ILiteDatabase>();
        liteCollection ??= Substitute.For<ILiteCollection<PointDto>>();
        var pointDtos = new List<PointDto> { new() { Id = 1, X = 1, Y = 2 } };
        liteDbContext.Database.Returns(liteDatabase);
        liteDatabase.GetCollection<PointDto>(Arg.Any<string>()).Returns(liteCollection);
        liteCollection.FindAll().Returns(pointDtos);
        var cut = new PlaneService(liteDbContext);
        return cut;
    }
}
