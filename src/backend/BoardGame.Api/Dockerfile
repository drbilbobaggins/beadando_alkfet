# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project file first for better layer caching
COPY ["BoardGame.Api.csproj", "./"]
RUN dotnet restore "BoardGame.Api.csproj"

# Copy the rest of the source code
COPY . .

# Publish the application
RUN dotnet publish "BoardGame.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "BoardGame.Api.dll"]
