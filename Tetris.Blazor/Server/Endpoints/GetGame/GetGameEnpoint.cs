using FastEndpoints;
using Tetris.Blazor.Server.Endpoints.Common;
using Tetris.Blazor.Shared.Entities;

namespace Tetris.Blazor.Server.Endpoints.GetGame
{
  public class GetGameEnpoint : EndpointWithoutRequest<GameDto, GameDtoMapper>
  {
    private readonly IGameStorage _storage;

    public GetGameEnpoint(IGameStorage storage)
    {
      _storage = storage;
    }

    public override void Configure()
    {
      Get("api/game/{GameId}");
      AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
      var game = await _storage.OneBy(Route<Guid>("GameId"));
      await SendAsync(Map.FromEntity(game));
    }
  }
}
