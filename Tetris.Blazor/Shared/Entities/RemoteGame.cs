using Tetris.Blazor.Shared.SignalR;

namespace Tetris.Blazor.Shared.Entities;

public class RemoteGame : IRemoteGame
{
  public int Score { get; set; }
  public int Level { get; set; }
  public event Action? Updated;

  private Cell[][] _grid;

  public RemoteGame()
  {
    var RowCount = 20;
    var ColumnCount = 10;

    _grid = new Cell[RowCount][];
    for (int i = 0; i < RowCount; i++)
      _grid[i] = new Cell[ColumnCount];

    for (var x = 0; x < RowCount; x++)
    for (var y = 0; y < ColumnCount; y++)
      _grid[x][y] = new Cell(x, y);
  }

  public IEnumerable<Cell> Field()
  {
    foreach (var row in _grid)
    {
      foreach (var cell in row)
      {
        yield return cell;
      }
    }
  }

  public void ApplyUpdate(Update update)
  {
    _grid = update.Field;
    Score = update.Score;
    Level = update.Level;

    Updated?.Invoke();
  }
}