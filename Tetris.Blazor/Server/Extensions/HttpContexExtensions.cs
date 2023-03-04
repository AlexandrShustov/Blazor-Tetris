using FluentResults;

namespace Tetris.Blazor.Server.Extensions
{
  public static class HttpContexExtensions
  {
    public const string GameIdQueryParameterName = "gameId";

    public static Result<Guid> GameIdOrDefault(this HttpContext self)
    {
      if (self.Request.Query.TryGetValue(GameIdQueryParameterName, out var value))
      {
        return Result.Ok(Guid.Parse(value.ToString()));
      }

      return Result.Fail("Unable to get a gameId from the query string");
    }
  }
}
