namespace Tetris.Blazor.Shared.Entities;

public class OfflineGameLevel : IGameLevel
{
  public int CurrentLevel => _currentLevel;

  private const int Default = 1;
  private int _currentLevel;

  public OfflineGameLevel(Game game)
  {
    SetDefaults();
    game.ScoreUpdated += () => Update(game.Score);
  }

  public void Update(int newValue)
  {
    _currentLevel = Math.Max(1, newValue / 2);
  }

  public void Reset()
  {
    SetDefaults();
  }

  private void SetDefaults()
  {
    _currentLevel = Default;
  }
}
