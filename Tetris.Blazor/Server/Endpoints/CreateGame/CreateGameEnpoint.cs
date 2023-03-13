using FastEndpoints;
using Tetris.Blazor.Server.Endpoints.Common;
using Tetris.Blazor.Shared.Entities;
using Game = Tetris.Blazor.Server.Entities.Game;

namespace Tetris.Blazor.Server.Endpoints.CreateGame
{
  public class CreateGameEnpoint : Endpoint<NewGameDto, GameDto, GameDtoMapper>
  {
    private readonly IGameStorage _storage;

    public CreateGameEnpoint(IGameStorage storage)
    {
      _storage = storage;
    }

    public override void Configure()
    {
      Post("api/games");
      AllowAnonymous();
    }

    public override async Task HandleAsync(NewGameDto req, CancellationToken ct)
    {
      var game = await _storage.Create(new Game
      {
        Name = req.Name,
        IsPrivate = req.IsPrivate,
      });

      if (game == null)
      { 
        await SendAsync(null, statusCode: 200, cancellation: ct);
      }

      await SendAsync(Map.FromEntity(game), statusCode: 200);
    }
  }
}
