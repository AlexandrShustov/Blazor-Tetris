namespace Tetris.Blazor.Shared.Entities;

public class O : Figure
{
  public override Position[] Position { get; protected set; } = {
    new(2, 4), new(2, 5),
    new(3, 4), new(3, 5, pivot:true)
  };
}