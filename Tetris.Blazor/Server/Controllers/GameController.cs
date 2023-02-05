using Microsoft.AspNetCore.Mvc;
using Tetris.Blazor.Shared.Entities;
using Game = Tetris.Blazor.Server.Entities.Game;

namespace Tetris.Blazor.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
  private readonly IGameStorage _gameStorage;
  
  public GameController(IGameStorage gameStorage)
  {
    _gameStorage = gameStorage;
  }

  [HttpPost("create")]
  public async Task<IActionResult> CreateGame([FromBody] NewGameDto game)
  {
    var entity = new Game()
    {
      Name = game.Name,
      IsPrivate = game.IsPrivate,
    };

    var result = await _gameStorage.Create(entity);

    return result switch
    {
      { } => Ok(new GameDto()
      {
        Id = result.Id,
        IsPrivate = result.IsPrivate,
        Name = result.Name
      }),
      _ => BadRequest()
    };
  }

  [HttpGet("all")]
  public async Task<IActionResult> GetWaitingGames()
  {
    var all = await _gameStorage.All();

    return Ok(all.Select(Map));

    GameDto Map(Game game) => new()
    {
      Name = game.Name,
      Id = game.Id,
      IsPrivate = game.IsPrivate
    };
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetSingle([FromRoute] string id)
  {
    var game = await _gameStorage.OneBy(Guid.Parse(id));

    return game switch
    {
      { } x => Ok(Map(x!)),
      _ => NotFound()
    };

    GameDto Map(Game entity) => new()
    {
      Name = entity.Name,
      Id = entity.Id,
      IsPrivate = entity.IsPrivate
    };
  }
}