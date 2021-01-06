using Domain.Commands;
using FluentValidation;

namespace WebApi.Validators.Commands
{
    public class CompleteTaskCommandValidator: AbstractValidator<CompleteTaskCommand>
    {
        public CompleteTaskCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}