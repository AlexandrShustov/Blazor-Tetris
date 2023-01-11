namespace Tetris.Blazor.Shared.Entities;

public class I : Figure
{
  public override Position[] Position { get; protected set; } = new Position[]
  {
    new(0, 4),
    new(1, 4, pivot:true),
    new(2, 4),
    new(3, 4),
  };
}