namespace Tetris.Blazor.Server.Entities;

public class Game
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public bool IsPrivate { get; set; }
  public GameState State { get; set; }

  public Dictionary<string, Player> Players { get; set; }

  public Game()
  {
    Players = new Dictionary<string, Player>();
  }

  public Player? PlayerWith(string connection) => 
    Players.TryGetValue(connection, out var player) 
      ? player
      : null;

  public bool EveryoneIsReady() =>
    Players.Values.All(x => x.IsReady);

  public Player? OpponentOf(string connection) => 
    Players.Values.FirstOrDefault(x => x.ConnectionId != connection);

  internal void ResetReadyStatus()
  {
    foreach (var player in Players.Values)
    {
      player.IsReady = false;
    }
  }
}