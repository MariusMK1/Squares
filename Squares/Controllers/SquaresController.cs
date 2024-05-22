using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Squares.Models;
using Squares.Services;
using System.Net;

namespace Squares.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class SquaresController(ISquaresService squaresService) : ControllerBase
{
    private readonly ISquaresService _squaresService = squaresService;
    /// <summary>
    /// Returns a list of squares that can be drawn in the provided list of points
    /// </summary>
    /// <returns>a list of a list of points </returns>
    /// <remarks>
    /// Sample request:
    ///     GET /api/squares
    /// </remarks>
    [HttpGet]
    public List<List<Point>> Get()
    {
        return _squaresService.FindSquares();
    }
}
