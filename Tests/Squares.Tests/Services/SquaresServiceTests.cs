using Squares.Models;
using Squares.Services;
using System.Diagnostics.CodeAnalysis;

namespace Squares.Tests.Services;
[ExcludeFromCodeCoverage]
public class SquaresServiceTests
{
    [Fact]
    public void FindSquares_ReturnsListOfAListOfPoints()
    {
        var planeService = Substitute.For<IPlaneService>();
        var planeMock = new List<Point>
        {
            new(0, 0),
            new(0, 1),
            new(0, 2),
            new(1, 0),
            new(1, 1),
            new(1, 2),
            new(2, 0),
            new(2, 1),
            new(2, 2)
        };
        planeService.GetPoints().Returns(planeMock);
        var cut = new SquaresService(planeService);

        var result = cut.FindSquares();

        Assert.Equal(6, result.Count);
    }
}
