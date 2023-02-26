namespace Tetris.Blazor.Shared.Entities;

public class OnlineGameLevel : IGameLevel
{
  public int CurrentLevel => _level;

  private const int Default = 1;
  private int _level = Default;

  public void Update(int newValue)
  {
    _level = Math.Max(1, newValue / 2);
  }

  public void Reset()
  {
    _level = Default;
  }
}
