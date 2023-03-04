namespace Tetris.Blazor.Shared.SignalR;

public class Method
{
  public static Client Client { get; set; } = new Client();
  public static string JoinGame { get; set; } = "JoinGame";
  public static string Ready { get; set; } = "Ready";
  public static string Update { get; set; } = "Update";
  public static string ScoreUpdated { get; set; } = "ScoreUpdated";
  public static string GameOver { get; set; } = "GameOver";
  public static string PlayAgain { get; set; } = "PlayAgain";
}

public class Client
{
  public string GameJoined { get; set; } = "GameJoined";
  public string GetReady { get; set; } = "GetReady";
  public string Start { get; set; } = "Start";
  public string HandleUpdate { get; set; } = "HandleUpdate";
  public string OpponentScored { get; set; } = "OpponentScored";
  public string GameOver { get; set; } = "GameOver";
  public string AcceptPlayAgain { get; set; } = "AcceptPlayAgain";
  public string OpponentHasDisconnected { get; set; } = "OpponentHasDisconnected";
}