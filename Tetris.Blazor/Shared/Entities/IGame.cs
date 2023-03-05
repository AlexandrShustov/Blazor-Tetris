using Tetris.Blazor.Shared.SignalR;

namespace Tetris.Blazor.Shared.Entities;

public interface IGame
{
  public int Score { get; }
  public int Level { get; }

  event Action Updated;
  IEnumerable<Cell> Field();
}

public interface ILocalGame : IGame
{
  IGameLevel GameLevel { get; set; }

  event Action<int> ScoreUpdated;
  event Action GameOver;
  void HandleInput(string key);
  void Start();
  void Stop();
}

public interface IRemoteGame : IGame
{
  void HandleUpdate(Update update);
}

public interface IGameLevel
{
  int CurrentLevel { get; }

  void Reset();
  void Update(int newValue); 
}