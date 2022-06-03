using MediatR;
using Microsoft.AspNet.Identity;
using Project.Application.Exceptions;
using Project.Application.Utilities;
using Project.Core.Commands;
using Project.Core.Entities;
using Project.Domain.Users;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Application.Users.CreateUserUseCase
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserViewModel>
    {
        private readonly IRepository<User> _repository; 
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public CreateUserHandler(IRepository<User> repository, IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserViewModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var emailExists = await _userRepository.GetByEmail(request.Email);

            if (emailExists != null)
            {
                throw new ApplicationDataException("User with the same email already exists");
            }

            if (!request.Validate().IsValid)
            {
                //throw new ApplicationDataException(request.Validate().Errors);
                foreach (var error in request.Validate().Errors)
                {
                    throw new ApplicationDataException(error.ErrorMessage);
                }
            }

            var user = new User(request.Name, request.Email, _passwordHasher.HashPassword(request.Password), request.Roles);

            _repository.Create(user);
            await _repository.Save();

            return new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Token = JwtGenerator.GenerateAuthToken(user.Id.ToString()),
            };
        }
    }
}
