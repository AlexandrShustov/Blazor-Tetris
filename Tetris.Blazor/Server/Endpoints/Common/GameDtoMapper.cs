using FastEndpoints;
using Tetris.Blazor.Shared.Entities;
using Game = Tetris.Blazor.Server.Entities.Game;

namespace Tetris.Blazor.Server.Endpoints.Common
{
  public class GameDtoMapper : ResponseMapper<GameDto, Game>
  {
    public override GameDto FromEntity(Game e)
    {
      return new GameDto
      {
        Id = e.Id,
        Name = e.Name,
        IsPrivate = e.IsPrivate
      };
    }
  }
}
