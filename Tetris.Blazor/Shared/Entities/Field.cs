namespace Tetris.Blazor.Shared.Entities;

public class Field
{
  public int RowCount { get; init; }
  public int ColumnCount { get; init; }

  private readonly Cell[][] _grid;

  public Field(int rowCount, int columnCount)
  {
    RowCount = rowCount;
    ColumnCount = columnCount;

    _grid = new Cell[rowCount][];
    for (int i = 0; i < rowCount; i++)
      _grid[i] = new Cell[columnCount];

    for (var x = 0; x < RowCount; x++)
    for (var y = 0; y < ColumnCount; y++)
      _grid[x][y] = new Cell(x, y);
  }

  public Cell? ElementAt(Position pos)
  {
    if (!IsInBoundaries(pos.X, pos.Y))
      return null;

    return _grid[pos.X][pos.Y];
  }

  public Cell[] GetBy(Position[] positions)
  {
    if (positions.Any(x => !IsInBoundaries(x.X, x.Y)))
    {
      return Array.Empty<Cell>();
    }

    return positions.Select(x => ElementAt(x)!).ToArray();
  }

  public IEnumerable<Cell> Iterate()
  {
    for (var x = 4; x < RowCount; x++)
    for (var y = 0; y < ColumnCount; y++)
      yield return _grid[x][y];
  }

  public Cell[] GetRow(int index)
  {
    return _grid[index];
  }

  public void Eliminate(params int[] rows)
  {
    foreach (var index in rows.OrderBy(x => x))
    {
      var row = _grid[index].ToList();
      row.ForEach(x => x.Release());

      RaiseRowUp(index);
    }
  }

  public void Clear()
  {
    for (var x = 0; x < RowCount; x++)
    for (var y = 0; y < ColumnCount; y++)
      _grid[x][y].Figure = null;
  }

  private bool IsInBoundaries(int x, int y) =>
    x >= 0 && x < RowCount && y >= 0 && y < ColumnCount;

  private void RaiseRowUp(int index)
  {
    var newIndex = index;

    var rowUpper = _grid[newIndex - 1].ToList();
    var isUpperFree = rowUpper.All(x => x.State == State.Empty);

    while (isUpperFree == false)
    {
      (_grid[newIndex - 1], _grid[newIndex]) = (_grid[newIndex], _grid[newIndex - 1]);

      _grid[newIndex - 1].ToList()
        .ForEach(x => x.Position = new Position(newIndex - 1, x.Position.Y));

      _grid[newIndex].ToList()
        .ForEach(x => x.Position = new Position(newIndex, x.Position.Y));

      newIndex -= 1;
      if (newIndex <= 1)
        return;

      rowUpper = _grid[newIndex - 1].ToList();
      isUpperFree = rowUpper.All(x => x.State == State.Empty);
    }
  }
}