namespace Tetris.Blazor.Server.Entities;

public class Game
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public bool IsPrivate { get; set; }
  public State State { get; set; }

  public Player? PlayerOne { get; set; }
  public Player? PlayerTwo { get; set; }

  public Player? PlayerWith(string connection)
  {
    if (PlayerOne?.ConnectionId == connection )
      return PlayerOne;

    if (PlayerTwo?.ConnectionId == connection)
      return PlayerTwo;

    return null;
  }

  public IEnumerable<Player?> Players()
  {
    yield return PlayerOne;
    yield return PlayerTwo;
  }

  public bool EveryoneIsReady() => 
    (PlayerOne?.IsReady ?? false) && (PlayerTwo?.IsReady ?? false);
}