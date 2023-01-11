namespace Tetris.Blazor.Shared.Entities;

public abstract class Figure
{
  public abstract Position[] Position { get; protected set; }

  public static Position[] RotationMatrix = new Position[2]
  {
    new(0, -1),
    new(1, 0)
  };

  public void SetPosition(Position[] position) =>
    Position = position;

  public static Position[] NextDownPosition(Position[] currentPos) =>
    currentPos.Select(x => new Position(x.X + 1, x.Y, x.IsPivot)).ToArray();

  public static Position[] NextRightPosition(Position[] currentPos) =>
    currentPos.Select(x => new Position(x.X, x.Y + 1, x.IsPivot)).ToArray();

  public static Position[] NextLeftPosition(Position[] currentPos) =>
    currentPos.Select(x => new Position(x.X, x.Y - 1, x.IsPivot)).ToArray();

  public static Position[] NextRotation(Position[] currentPos)
  {
    var pivot = currentPos.First(x => x.IsPivot);
    var result = new List<Position>() { pivot };

    foreach (var position in currentPos.Where(x => !x.IsPivot))
    {
      var relativePos = position - pivot;
      var x = (RotationMatrix[0].X * relativePos.X) + (RotationMatrix[1].X * relativePos.Y);
      var y = (RotationMatrix[0].Y * relativePos.X) + (RotationMatrix[1].Y * relativePos.Y);

      var newPos = new Position(x, y);
      result.Add(newPos + pivot);
    }

    return result.ToArray();
  }
}