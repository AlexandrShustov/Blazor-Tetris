namespace Tetris.Blazor.Shared.Entities;

public class OnlineGameLevel : IGameLevel
{
  public int CurrentLevel => _level;

  private const int Default = 1;
  private int _level = Default;

  private int _currentScore = 0;

  public void Update(int newValue)
  {
    if (newValue > 0)
      _currentScore += newValue;

    _level = Math.Max(1, _currentScore / 2);
  }

  public void Reset()
  {
    _level = Default;
  }
}
