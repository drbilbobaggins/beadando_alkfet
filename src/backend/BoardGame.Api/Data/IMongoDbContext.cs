using MongoDB.Driver;
using BoardGameModel = BoardGame.Api.Models.BoardGame;

namespace BoardGame.Api.Data;

public interface IMongoDbContext
{
    IMongoCollection<BoardGameModel> BoardGames { get; }
}
