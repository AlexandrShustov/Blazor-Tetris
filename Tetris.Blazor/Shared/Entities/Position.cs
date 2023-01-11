namespace Tetris.Blazor.Shared.Entities;

public struct Position
{
  public int X { get; set; }
  public int Y { get; set; }
  public bool IsPivot { get; set; }

  public Position(int x, int y, bool pivot = false)
  {
    X = x;
    Y = y;
    IsPivot = pivot;
  }

  public void Deconstruct(out int x, out int y)
  {
    x = X;
    y = Y;
  }

  public static Position operator -(Position a, Position b) => new Position(a.X - b.X, a.Y - b.Y);
  public static Position operator +(Position a, Position b) => new Position(a.X + b.X, a.Y + b.Y);
}