using Squares.Controllers;
using Squares.Models;
using Squares.Services;
using System.Diagnostics.CodeAnalysis;

namespace Squares.Tests.Controlers;
[ExcludeFromCodeCoverage]
public sealed class SquaresControllerTests
{
    [Fact]
    public void Get_ReturnsListOfAListOfPoints()
    {
        // Arrange
        var squaresService = Substitute.For<ISquaresService>();
        var squaresMock = new List<List<Point>> { new() { new(1, 2) } };
        squaresService.FindSquares().Returns(squaresMock);
        var cut = new SquaresController(squaresService);

        // Act
        var result = cut.Get();

        // Assert
        Assert.IsType<List<List<Point>>>(result);
        Assert.Equal(squaresMock, result);
    }
}
