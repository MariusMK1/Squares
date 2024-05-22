using Microsoft.AspNetCore.Mvc;
using NSubstitute.ExceptionExtensions;
using Squares.Controllers;
using Squares.Models;
using Squares.Services;
using System.Diagnostics.CodeAnalysis;

namespace Squares.Tests.Controlers;
[ExcludeFromCodeCoverage]
public sealed class PlaneControllerTests
{
    private List<Point> _pointsMock = [new(1, 2)];
    private IPlaneService _planeService = Substitute.For<IPlaneService>();
    [Fact]
    public void Get_ReturnsListOfPoints()
    {
        _planeService.GetPoints().Returns(_pointsMock);
        var cut = new PlaneController(_planeService);

        var result = cut.Get();

        Assert.IsType<List<Point>>(result);
        Assert.Equal(_pointsMock, result);
    }
    [Fact]
    public void Create_ReturnsStatusCode201()
    {
        var cut = new PlaneController(_planeService);

        var result = cut.Create(_pointsMock) as ObjectResult;

        _planeService.Received().Create(_pointsMock);
        Assert.Equal(201, result?.StatusCode);
    }
    [Fact]
    public void Add_ReturnsStatusCode200()
    {
        var cut = new PlaneController(_planeService);

        var result = cut.Add(_pointsMock) as ObjectResult;

        _planeService.Received().Insert(_pointsMock);
        Assert.Equal(200, result?.StatusCode);
    }
    [Fact]
    public void Add_ThrowsException()
    {
        _planeService.Insert(_pointsMock).Throws(new Exception());
        var cut = new PlaneController(_planeService);

        var result = cut.Add(_pointsMock) as ObjectResult;

        Assert.Equal(400, result?.StatusCode);
    }
    [Fact]
    public void Delete_ReturnsStatusCode200()
    {
        var cut = new PlaneController(_planeService);

        var result = cut.Delete(_pointsMock[0]) as ObjectResult;

        _planeService.Received().Delete(_pointsMock[0]);
        Assert.Equal(200, result?.StatusCode);
    }
    [Fact]
    public void Delete_ThrowsException()
    {
        _planeService.Delete(_pointsMock[0]).Throws(new Exception());
        var cut = new PlaneController(_planeService);

        var result = cut.Delete(_pointsMock[0]) as ObjectResult;

        Assert.Equal(400, result?.StatusCode);
    }
}
