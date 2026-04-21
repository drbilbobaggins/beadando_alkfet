using BoardGame.Api.Models;
using BoardGameModel = BoardGame.Api.Models.BoardGame;

namespace BoardGame.Api.Services;

public interface IBoardGameService
{
    Task<List<BoardGameModel>> GetAllAsync();

    Task<BoardGameModel?> GetByIdAsync(string id);

    Task<BoardGameModel> CreateAsync(CreateBoardGameRequest request);

    Task<bool> UpdateAsync(string id, UpdateBoardGameRequest request);

    Task<bool> DeleteAsync(string id);
}
