using BoardGame.Api.Configuration;
using BoardGame.Api.Data;
using BoardGame.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDb"));
builder.Services.AddScoped<IMongoDbContext, MongoDbContext>();
builder.Services.AddScoped<IBoardGameRepository, BoardGameRepository>();
builder.Services.AddScoped<IBoardGameService, BoardGameService>();

builder.Services.AddControllers();

// CORS config
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// 👇 CORS middleware (IMPORTANT: before MapControllers !)
app.UseCors("FrontendPolicy");

app.MapControllers();

app.MapGet("/", () => "BoardGame API is running.");

app.Run();
