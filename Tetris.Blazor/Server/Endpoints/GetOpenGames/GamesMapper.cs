using FastEndpoints;
using Tetris.Blazor.Server.Endpoints.Common;
using Tetris.Blazor.Shared.Entities;
using Game = Tetris.Blazor.Server.Entities.Game;

namespace Tetris.Blazor.Server.Endpoints.GetOpenGames
{
  public class GamesMapper : ResponseMapper<IEnumerable<GameDto>, IEnumerable<Game>>
  {
    public override IEnumerable<GameDto> FromEntity(IEnumerable<Game> e) =>
      e.Select(x => Resolve<GameDtoMapper>().FromEntity(x));
  }
}
