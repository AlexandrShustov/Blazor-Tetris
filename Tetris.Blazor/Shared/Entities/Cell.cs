namespace Tetris.Blazor.Shared.Entities;

public class Cell
{
  public State State => Figure is null ? State.Empty : State.Filled;
  public Figure? Figure { get; set; }

  public Position Position { get; set; }

  public Cell(int x, int y)
  {
    Position = new Position(x, y);
  }

  public void Occupy(Figure newFigure)
  {
    if (Figure != null)
      throw new InvalidOperationException("Impossible to occupy an occupied cell.");

    Figure = newFigure;
  }

  public void Release()
  {
    Figure = null;
  }

  public bool IsFreeFor(Figure figure) => 
    State == State.Empty || Figure == figure;
}

public enum State
{
  Empty = 1,
  Filled = 2
}