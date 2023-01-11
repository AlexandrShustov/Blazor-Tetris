namespace Tetris.Blazor.Shared.Entities;

public class Z : Figure
{
  public override Position[] Position { get; protected set; } =
  {
    new(2, 3), new(2, 4),
                   new(3, 4, pivot : true), new(3, 5),
  };
}