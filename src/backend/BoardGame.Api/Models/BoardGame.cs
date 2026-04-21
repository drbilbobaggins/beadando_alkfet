using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BoardGame.Api.Models;

public class BoardGame
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("title")]
    public string Title { get; set; } = string.Empty;

    [BsonElement("publisher")]
    public string Publisher { get; set; } = string.Empty;

    [BsonElement("minPlayers")]
    public int MinPlayers { get; set; }

    [BsonElement("maxPlayers")]
    public int MaxPlayers { get; set; }

    [BsonElement("estimatedPlayTime")]
    public int EstimatedPlayTime { get; set; }

    [BsonElement("category")]
    public string Category { get; set; } = string.Empty;
}
