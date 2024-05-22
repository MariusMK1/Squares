using LiteDB;
using Microsoft.Extensions.Options;

namespace Squares.LiteDb;

public sealed class LiteDbContext(IOptions<LiteDbOptions> options) : ILiteDbContext
{
    public ILiteDatabase Database { get; } = new LiteDatabase(options.Value.DatabaseLocation);
}