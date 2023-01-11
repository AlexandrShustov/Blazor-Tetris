namespace Tetris.Blazor.Shared.Entities;

public class InputMap
{
  private Dictionary<string, MoveType> _moveTypes;

  public InputMap()
  {
    _moveTypes = new Dictionary<string, MoveType>()
    {
      ["ArrowLeft"] = MoveType.Left,
      ["ArrowRight"] = MoveType.Right,
      ["ArrowUp"] = MoveType.Rotate,
      [" "] = MoveType.Ground,
    };
  }

  public MoveType? GetMoveTypeBy(string pressedKeyCode)
  {
    if (_moveTypes.TryGetValue(pressedKeyCode, out var key))
      return key;

    return null;
  }
}