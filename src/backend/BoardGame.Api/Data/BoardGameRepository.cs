using MongoDB.Driver;
using BoardGameModel = BoardGame.Api.Models.BoardGame;

namespace BoardGame.Api.Data;

public class BoardGameRepository : IBoardGameRepository
{
    private readonly IMongoCollection<BoardGameModel> _boardGamesCollection;

    public BoardGameRepository(IMongoDbContext mongoDbContext)
    {
        // The repository keeps MongoDB details out of upper layers.
        _boardGamesCollection = mongoDbContext.BoardGames;
    }

    public async Task<List<BoardGameModel>> GetAllAsync()
    {
        // MongoDB operations are async to avoid blocking request threads during I/O.
        return await _boardGamesCollection.Find(_ => true).ToListAsync();
    }

    public async Task<BoardGameModel?> GetByIdAsync(string id)
    {
        return await _boardGamesCollection.Find(boardGame => boardGame.Id == id).FirstOrDefaultAsync();
    }

    public async Task<BoardGameModel> CreateAsync(BoardGameModel boardGame)
    {
        await _boardGamesCollection.InsertOneAsync(boardGame);
        return boardGame;
    }

    public async Task<bool> UpdateAsync(string id, BoardGameModel boardGame)
    {
        boardGame.Id = id;

        var result = await _boardGamesCollection.ReplaceOneAsync(existingBoardGame => existingBoardGame.Id == id, boardGame);
        return result.MatchedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _boardGamesCollection.DeleteOneAsync(boardGame => boardGame.Id == id);
        return result.DeletedCount > 0;
    }
}
