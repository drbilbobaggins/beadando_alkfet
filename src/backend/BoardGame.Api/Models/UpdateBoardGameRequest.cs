namespace BoardGame.Api.Models;

public class UpdateBoardGameRequest
{
    public string Title { get; set; } = string.Empty;

    public string Publisher { get; set; } = string.Empty;

    public int MinPlayers { get; set; }

    public int MaxPlayers { get; set; }

    public int EstimatedPlayTime { get; set; }

    public string Category { get; set; } = string.Empty;
}
