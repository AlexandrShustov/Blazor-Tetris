namespace Tetris.Blazor.Shared.Entities;

public class GameDto
{
  public Guid Id { get; set; }
  public string CreatedBy { get; set; }
  public string Name { get; set; }
  public bool IsPrivate { get; set; }
}