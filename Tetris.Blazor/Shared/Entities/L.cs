namespace Tetris.Blazor.Shared.Entities;

public class L : Figure
{
  public override Position[] Position { get; protected set; } =
  {
    new(2, 4),
    new(3, 4, pivot:true),
    new(4, 4),            new(4, 5),
  };
}