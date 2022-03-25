using MediatR;
using Project.Application.Exceptions;
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

        public CreateUserHandler(IRepository<User> repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
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

            var user = new User(request.Name, request.Email, request.Password, "Users");

            _repository.Create(user);
            await _repository.Save();

            return new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            };
        }
    }
}
