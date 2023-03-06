using Microsoft.AspNetCore.SignalR;
using Tetris.Blazor.Server.Entities;
using Tetris.Blazor.Server.Extensions;
using Tetris.Blazor.Shared.Entities;
using Tetris.Blazor.Shared.SignalR;

namespace Tetris.Blazor.Server.Hubs;

public class GameHub : Hub
{
  private readonly IGameStorage _storage;

  public GameHub(IGameStorage storage)
  {
    _storage = storage;
  }

  public async Task JoinGame(Guid gameId)
  {
    var game = await _storage.OneBy(gameId);
    if (game is null || game.Players.Count == 2)
      return;

    game.Players.Add(Context.ConnectionId, new Player
    {
      ConnectionId = Context.ConnectionId
    });

    if (game.Players.Count == 2) 
    {
      await Clients
        .Clients(game.Players.Keys)
        .SendAsync(Method.Client.GetReady, game.Id);

      return;
    }

    await Clients
      .Caller
      .SendAsync(Method.Client.GameJoined, game.Id);
  }

  public async Task Ready(Guid gameId)
  {
    var game = await _storage.OneBy(gameId);
    if (game is null)
      return;

    var connection = Context.ConnectionId;
    var player = game.PlayerWith(connection);
    if (player is null)
      return;

    player.IsReady = true;

    if (game.EveryoneIsReady())
    {
      var connections = game.Players.Keys;

      await Clients.Clients(connections).SendAsync(Method.Client.Start, game.Id);
      game.State = GameState.Playing;
    }
  }

  public async Task Update(Update? update)
  {
    var result = Context.GetHttpContext()!.GameIdOrDefault();
    if (result.IsFailed)
      return;

    var game = await _storage.OneBy(result.Value);
    if (game?.PlayerWith(Context.ConnectionId) is null)
      return;

    var opponent = game?.OpponentOf(Context.ConnectionId);
    await Clients.Client(opponent!.ConnectionId).SendAsync(Method.Client.HandleUpdate, update);
  }

  public async Task ScoreUpdated(Update update)
  {
    var result = Context.GetHttpContext()!.GameIdOrDefault();
    if (result.IsFailed)
      return;

    var game = await _storage.OneBy(result.Value);
    if (game is null)
      return;

    var opponent = game.OpponentOf(Context.ConnectionId);
    await Clients.Client(opponent!.ConnectionId).SendAsync(Method.Client.HandleUpdate, update);
  }

  public async Task GameOver(Update update)
  {
    var result = Context.GetHttpContext()!.GameIdOrDefault();
    if (result.IsFailed)
      return;

    var game = await _storage.OneBy(result.Value);
    if (game == null)
      return;

    game.ResetReadyStatus();

    var gameResult = new OnlineGameResult
    {
      IsVictory = true
    };

    var opponent = game.OpponentOf(Context.ConnectionId);

    await Clients.Client(opponent!.ConnectionId).SendAsync(Method.Client.GameOver, gameResult);

    gameResult.IsVictory = false;
    await Clients.Client(Context.ConnectionId).SendAsync(Method.Client.GameOver, gameResult);
  }

  public override async Task OnDisconnectedAsync(Exception? exception)
  {
    var result = Context.GetHttpContext()!.GameIdOrDefault();

    var gameId = result.IsSuccess 
      ? result.Value 
      : (Guid?) null;

    if (result.IsFailed)
      return;

    var game = await _storage.OneBy(result.Value);
    var opponent = game.OpponentOf(Context.ConnectionId);

    if (opponent != null)
      await Clients.Client(opponent.ConnectionId)
        .SendAsync(Method.Client.OpponentHasDisconnected);

    return;
  }
}