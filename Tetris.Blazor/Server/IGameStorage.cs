using System.Collections.Concurrent;
using Tetris.Blazor.Server.Entities;

namespace Tetris.Blazor.Server;

public interface IGameStorage
{
  Task<Game?> Create(Game game);
  Task<IEnumerable<Game>> OpenedGames();
  Task<Game?> OneBy(Guid id);
}

public class GameStorage : IGameStorage
{
  private readonly ConcurrentDictionary<Guid, Game> _games;

  public GameStorage()
  {
    _games = new ConcurrentDictionary<Guid, Game>();
  }

  public Task<Game?> Create(Game game)
  {
    game.Id = Guid.NewGuid();
    game.State = GameState.Waiting;
    
    _games.TryAdd(game.Id, game);
    return Task.FromResult<Game?>(game);
  }

  public Task<IEnumerable<Game>> OpenedGames()
  {
    var openAvailableGames = _games.Values
      .Where(x => !x.IsPrivate)
      .Where(x => x.State == GameState.Waiting)
      .ToList();

    return Task.FromResult(openAvailableGames as IEnumerable<Game>);
  }

  public Task<Game?> OneBy(Guid id)
  {
    return Task.FromResult<Game?>(_games.Values.FirstOrDefault(x => x.Id == id));
  }
}