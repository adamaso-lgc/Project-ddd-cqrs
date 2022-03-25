using FluentValidation.Results;
using MediatR;
using Moq;
using NUnit.Framework;
using Project.Application.Exceptions;
using Project.Application.Users;
using Project.Application.Users.CreateUserUseCase;
using Project.Core.Entities;
using Project.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Tests.Application.Users
{
    [TestFixture]
    public class CreateUserTest
    {
        private Mock<IRepository<User>> _repository;
        private Mock<IUserRepository> _userRepository;

        [SetUp]
        public void SetUp()
        {
            _repository = new Mock<IRepository<User>>();
            _userRepository = new Mock<IUserRepository>();
        }

        [Test]
        public async Task CreateUser_UserNotExists_ReturnNewUserAsync()
        {
            // Arrange
            var name = "André";
            var email = "andre@gmail.com";

            _userRepository.Setup(repo => repo.GetByEmail(email)).ReturnsAsync((User)null);

            var command = new CreateUserCommand(name, email, "1234");
            var handler = new CreateUserHandler(_repository.Object, _userRepository.Object);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Console.WriteLine(result.Id);
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.TypeOf<Guid>());
            Assert.That(result.Name, Is.EqualTo(name));

        }

        [Test]
        public void CreateUser_UserExists_ThrowApplicationDataExeption()
        {
            var name = "André";
            var email = "andre@gmail.com";

            _userRepository.Setup(repo => repo.GetByEmail(email)).ReturnsAsync(
                new User("André", "andre@gmail.com", "1234", "Users"));

            var command = new CreateUserCommand(name, email, "1234");
            var handler = new CreateUserHandler(_repository.Object, _userRepository.Object);


            Assert.That(() => handler.Handle(command, CancellationToken.None), Throws.Exception.TypeOf<ApplicationDataException>());

        }

        [Test]
        public void CreateUser_InvalidCommands_ReturnFalse()
        {
            _userRepository.Setup(repo => repo.GetByEmail("andre@gmail.com")).ReturnsAsync((User)null);

            var command = new CreateUserCommand("", "andre@gmail.com", "1234");
            var handler = new CreateUserHandler(_repository.Object, _userRepository.Object);

            Assert.IsFalse(command.Validate().IsValid);
            Assert.That(() => handler.Handle(command, CancellationToken.None), Throws.Exception.TypeOf<ApplicationDataException>());

        }
    }
}
