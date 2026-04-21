using BoardGame.Api.Data;
using BoardGame.Api.Models;
using BoardGameModel = BoardGame.Api.Models.BoardGame;

namespace BoardGame.Api.Services;

public class BoardGameService : IBoardGameService
{
    private readonly IBoardGameRepository _boardGameRepository;

    public BoardGameService(IBoardGameRepository boardGameRepository)
    {
        _boardGameRepository = boardGameRepository;
    }

    public async Task<List<BoardGameModel>> GetAllAsync()
    {
        return await _boardGameRepository.GetAllAsync();
    }

    public async Task<BoardGameModel?> GetByIdAsync(string id)
    {
        return await _boardGameRepository.GetByIdAsync(id);
    }

    public async Task<BoardGameModel> CreateAsync(CreateBoardGameRequest request)
    {
        ValidateRequest(request.Title, request.Publisher, request.Category, request.MinPlayers, request.MaxPlayers, request.EstimatedPlayTime);

        // Validation and request-to-domain mapping stay in the service so controllers can remain thin.
        var boardGame = new BoardGameModel
        {
            Title = request.Title,
            Publisher = request.Publisher,
            MinPlayers = request.MinPlayers,
            MaxPlayers = request.MaxPlayers,
            EstimatedPlayTime = request.EstimatedPlayTime,
            Category = request.Category
        };

        return await _boardGameRepository.CreateAsync(boardGame);
    }

    public async Task<bool> UpdateAsync(string id, UpdateBoardGameRequest request)
    {
        ValidateRequest(request.Title, request.Publisher, request.Category, request.MinPlayers, request.MaxPlayers, request.EstimatedPlayTime);

        var boardGame = new BoardGameModel
        {
            Title = request.Title,
            Publisher = request.Publisher,
            MinPlayers = request.MinPlayers,
            MaxPlayers = request.MaxPlayers,
            EstimatedPlayTime = request.EstimatedPlayTime,
            Category = request.Category
        };

        return await _boardGameRepository.UpdateAsync(id, boardGame);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _boardGameRepository.DeleteAsync(id);
    }

    private static void ValidateRequest(
        string title,
        string publisher,
        string category,
        int minPlayers,
        int maxPlayers,
        int estimatedPlayTime)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title must not be empty.");
        }

        if (string.IsNullOrWhiteSpace(publisher))
        {
            throw new ArgumentException("Publisher must not be empty.");
        }

        if (string.IsNullOrWhiteSpace(category))
        {
            throw new ArgumentException("Category must not be empty.");
        }

        if (minPlayers < 1)
        {
            throw new ArgumentException("MinPlayers must be greater than or equal to 1.");
        }

        if (maxPlayers < minPlayers)
        {
            throw new ArgumentException("MaxPlayers must be greater than or equal to MinPlayers.");
        }

        if (estimatedPlayTime <= 0)
        {
            throw new ArgumentException("EstimatedPlayTime must be greater than 0.");
        }
    }
}
