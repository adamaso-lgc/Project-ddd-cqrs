using FluentValidation;
using FluentValidation.Results;
using Project.Core.Commands;

namespace Project.Application.Users.CreateUserUseCase
{
    public class CreateUserCommand : ICommand<UserViewModel>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public CreateUserCommand(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public ValidationResult Validate()
        {
            return new CreateUserCommandValidator().Validate(this);
        }
    }

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is empty.")
            .Length(2, 100).WithMessage("The Name must have between 2 and 100 characters.");

            RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email is empty.")
            .Length(5, 100).WithMessage("The Email must have between 5 and 100 characters.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is empty.");
        }
    }

}
