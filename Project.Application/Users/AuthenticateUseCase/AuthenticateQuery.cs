using FluentValidation;
using FluentValidation.Results;
using Project.Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Users.AuthenticateUseCase
{
    public class AuthenticateQuery : IQuery<UserViewModel>
    {
        public string Email { get; init; }
        public string Password { get; init; }

        public ValidationResult Validate()
        {
            throw new NotImplementedException();
        }
    }

    public class CreateUserCommandValidator : AbstractValidator<AuthenticateQuery>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is empty.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Password is empty.");
        }
    }
}
