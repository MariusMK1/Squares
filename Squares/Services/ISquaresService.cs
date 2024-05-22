using Squares.Models;

namespace Squares.Services;

public interface ISquaresService
{
    List<List<Point>> FindSquares();
}
