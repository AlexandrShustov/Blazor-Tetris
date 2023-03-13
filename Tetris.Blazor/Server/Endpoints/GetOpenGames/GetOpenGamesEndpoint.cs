using FastEndpoints;
using Tetris.Blazor.Shared.Entities;

namespace Tetris.Blazor.Server.Endpoints.GetOpenGames
{
  public class GetOpenGamesEndpoint : EndpointWithoutRequest<IEnumerable<GameDto>, GamesMapper>
  {
    private readonly IGameStorage _storage;

    public GetOpenGamesEndpoint(IGameStorage storage)
    {
      _storage = storage;
    }

    public override void Configure()
    {
      Get("api/games/opened");
      AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct) =>
      await SendOkAsync(Map.FromEntity(await _storage.OpenedGames()), ct);
  }
}
