using BoardGame.Api.Configuration;
using BoardGame.Api.Data;
using BoardGame.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDb"));
builder.Services.AddScoped<IMongoDbContext, MongoDbContext>();
builder.Services.AddScoped<IBoardGameRepository, BoardGameRepository>();
builder.Services.AddScoped<IBoardGameService, BoardGameService>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.MapGet("/", () => "BoardGame API is running.");

app.Run();
