using BoardGameModel = BoardGame.Api.Models.BoardGame;

namespace BoardGame.Api.Data;

public interface IBoardGameRepository
{
    Task<List<BoardGameModel>> GetAllAsync();

    Task<BoardGameModel?> GetByIdAsync(string id);

    Task<BoardGameModel> CreateAsync(BoardGameModel boardGame);

    Task<bool> UpdateAsync(string id, BoardGameModel boardGame);

    Task<bool> DeleteAsync(string id);
}
