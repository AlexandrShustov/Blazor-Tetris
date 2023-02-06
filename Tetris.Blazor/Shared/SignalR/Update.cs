using Tetris.Blazor.Shared.Entities;

namespace Tetris.Blazor.Shared.SignalR;

public class Update
{
  public IEnumerable<Cell> Field { get; set; }
  public int Score { get; set; }
  public int Level { get; set; }
}