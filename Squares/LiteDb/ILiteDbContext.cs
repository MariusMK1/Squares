using LiteDB;

namespace Squares.LiteDb;

public interface ILiteDbContext
{
    ILiteDatabase Database { get; }
}