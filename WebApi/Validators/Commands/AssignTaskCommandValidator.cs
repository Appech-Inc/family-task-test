using Domain.Commands;
using FluentValidation;

namespace WebApi.Validators.Commands
{
    public class AssignTaskCommandValidator: AbstractValidator<AssignTaskCommand>
    {
        public AssignTaskCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.AssignedToId).NotEmpty();
        }
    }
}