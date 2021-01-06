using Domain.Commands;
using FluentValidation;

namespace WebApi.Validators.Commands
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(x => x.Subject).NotNull().NotEmpty();
        }
    }
}