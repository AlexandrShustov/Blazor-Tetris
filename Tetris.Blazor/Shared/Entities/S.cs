namespace Tetris.Blazor.Shared.Entities;

public class S : Figure
{
  public override Position[] Position { get; protected set; } =
  {
                   new(2, 4), new(2, 5),
    new(3, 3), new(3, 4, pivot : true),
  };
}