using Tetris.Blazor.Shared.SignalR;

namespace Tetris.Blazor.Shared.Entities;

public class RemoteGame : IRemoteGame
{
  public int Score { get; set; }
  public int Level { get; set; } = 1;
  public event Action? Updated;

  private readonly Cell[][] _grid;
  private readonly Figure _defaultFigure = new S();

  private readonly int _rowCount;
  private readonly int _columnCount;

  public RemoteGame()
  {
    _rowCount = 24;
    _columnCount = 10;

    _grid = new Cell[_rowCount][];

    for (int i = 0; i < _rowCount; i++)
      _grid[i] = new Cell[_columnCount];

    for (var x = 0; x < _rowCount; x++)
    for (var y = 0; y < _columnCount; y++)
      _grid[x][y] = new Cell(x, y);
  }

  public IEnumerable<Cell> Field()
  {
    for (var x = 4; x < _rowCount; x++)
    for (var y = 0; y < _columnCount; y++)
      yield return _grid[x][y];
  }

  public void HandleUpdate(Update update)
  {
    if (update.Field?.Any() == true)
    {
      var set = update.Field.ToHashSet();

      for (var i = 0; i < _rowCount; i++)
        for (var j = 0; j < _columnCount; j++)
        {
          var cell = _grid[i][j];

          if (!set.Contains(cell.Position))
            cell.Release();
          else if (cell.State == State.Empty)
            cell.Occupy(_defaultFigure);
        }
    }

    Score += update.Score;
    Level = Math.Max(update.Level, Level);

    Updated?.Invoke();
  }
}