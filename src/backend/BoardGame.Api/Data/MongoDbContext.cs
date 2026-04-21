using BoardGame.Api.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using BoardGameModel = BoardGame.Api.Models.BoardGame;

namespace BoardGame.Api.Data;

public class MongoDbContext : IMongoDbContext
{
    public MongoDbContext(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var settings = mongoDbSettings.Value;
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);

        // The context exposes collections so repositories can share one place for database access setup.
        BoardGames = database.GetCollection<BoardGameModel>(settings.BoardGamesCollectionName);
    }

    public IMongoCollection<BoardGameModel> BoardGames { get; }
}
