using FastEndpoints;
using FluentValidation;
using Tetris.Blazor.Shared.Entities;

namespace Tetris.Blazor.Server.Endpoints.CreateGame
{
  public class NewGameValidator : Validator<NewGameDto>
  {
    public NewGameValidator()
    {
      RuleFor(x => x.Name)
        .NotEmpty()
        .MaximumLength(100)
        .WithMessage("The name of the game should be not empty and less than 100 symbols length");
    }
  }
}
