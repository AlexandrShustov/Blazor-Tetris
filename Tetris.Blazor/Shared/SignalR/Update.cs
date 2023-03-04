using System.Text.Json.Serialization;
using Tetris.Blazor.Shared.Entities;

namespace Tetris.Blazor.Shared.SignalR;

public sealed class Update
{
  [JsonPropertyName("f")]
  public List<Position> Field { get; set; }

  [JsonPropertyName("s")]
  public int Score { get; set; }

  [JsonPropertyName("l")]
  public int Level { get; set; }

  public Update()
  {
    Field = new List<Position>();
  }

  public void Add(IEnumerable<Position> positions) => 
    Field.AddRange(positions);

  public void SetLevel(int level) => 
    Level = level;

  public void SetScore(int score) =>
    Score = score;

  public void Reset() => 
    Field.Clear();
}