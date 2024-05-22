using Microsoft.AspNetCore.Mvc;
using Squares.Models;
using Squares.Services;

namespace Squares.Controllers;
[ApiController]
[Route("api/[controller]")]
public sealed class PlaneController(IPlaneService planeService) : ControllerBase
{
    readonly IPlaneService _planeService = planeService;

    /// <summary>
    /// Gets all provided points of the plane.
    /// </summary>
    /// <returns>A list of points.</returns>
    /// <remarks>
    /// Sample request:
    ///     GET /api/points
    /// </remarks>
    [HttpGet]
    public List<Point> Get()
    {
        return _planeService.GetPoints();
    }
    /// <summary>
    /// Creates a new plane with the given collection of points.
    /// </summary>
    /// <param name="collection">The collection of points to create the plane with.</param>
    /// <returns>An ActionResult indicating the success of the operation.</returns>
    /// <remarks>
    /// Sample request:
    ///     POST /api/points
    ///     [
    ///         { "x": 1, "y": 2 },
    ///         { "x": 3, "y": 4 }
    ///     ]
    /// </remarks>
    [HttpPost]
    public ActionResult Create(List<Point> collection)
    {
        _planeService.Create(collection);
        return StatusCode(201, "Collection was created");
    }
    /// <summary>
    /// Adds a collection of points to the plane.
    /// </summary>
    /// <param name="collection">The collection of points to add to the plane.</param>
    /// <returns>An ActionResult indicating the success of the operation.</returns>
    /// <remarks>
    /// Sample request:
    ///     PUT /api/points
    ///     [
    ///         { "x": 5, "y": 6 },
    ///         { "x": 7, "y": 8 }
    ///     ]
    /// </remarks>
    [HttpPut]
    public ActionResult Add(List<Point> collection)
    {
        try
        {
            _planeService.Insert(collection);
            return Ok("List of points was added");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    /// <summary>
    /// Deletes a point from the plane.
    /// </summary>
    /// <param name="point">The point to delete from the plane.</param>
    /// <returns>An ActionResult indicating the success of the operation.</returns>
    /// <remarks>
    /// Sample request:
    ///     DELETE /api/points
    ///     {
    ///         "x": 1,
    ///         "y": 2
    ///     }
    /// </remarks>
    [HttpDelete]
    public ActionResult Delete(Point point)
    {
        try
        {
            _planeService.Delete(point);
            return Ok($"{point} has been deleted");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
