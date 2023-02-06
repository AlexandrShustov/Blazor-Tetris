using Microsoft.AspNetCore.SignalR;
using Tetris.Blazor.Server.Entities;
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
    if (game is null)
      return;

    if (game.PlayerOne is null)
      game.PlayerOne = new Player()
      {
        ConnectionId = Context.ConnectionId
      };
    else
    {
      game.PlayerTwo = new Player()
      {
        ConnectionId = Context.ConnectionId
      };
    }

    if (game.PlayerOne is not null && game.PlayerTwo is not null)
    {
      var connections = new[]
      {
        game.PlayerOne.ConnectionId,
        game.PlayerTwo.ConnectionId
      };

      await Clients.Clients(connections)
        .SendAsync("GetReady", game.Id);
    }

    await Clients.Caller.SendAsync("GameJoined", game.Id);
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
      var connections = game.Players()!
        .Select(x => x!.ConnectionId)
        .ToList();

      await Clients.Clients(connections).SendAsync("Start", game.Id);
      game.State = State.Playing;
    }
  }
}