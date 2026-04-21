namespace BoardGame.Api.Configuration;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = string.Empty;

    public string DatabaseName { get; set; } = string.Empty;

    public string BoardGamesCollectionName { get; set; } = string.Empty;
}
