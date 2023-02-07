using Tetris.Blazor.Shared.SignalR;

namespace Tetris.Blazor.Shared.Entities;

public interface IGame
{
  public int Score { get; set; }
  public int Level { get; set; }

  event Action Updated;
  IEnumerable<Cell> Field();
}

public interface ILocalGame : IGame
{
  void HandleInput(string key);
  void Start();
  void Stop();
}

public interface IRemoteGame : IGame
{
  void HandleUpdate(Update update);
}