using FluentValidation.Results;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Project.Application.Exceptions;
using Project.Application.Users;
using Project.Application.Users.CreateUserUseCase;
using Project.Core.Entities;
using Project.Domain.Enums;
using Project.Domain.Users;
using Project.Infra.Database.Context;
using Project.Infra.Domain;
using Project.Infra.Domain.Users;
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
        private DbContextOptions<ProjectContext> _dbContextOptions;
        private ProjectContext _context;
        private Repository<User> _repository;
        private UserRepository _userRepository;
        private PasswordHasher _passwordHasher;

        [SetUp]
        public void SetUp()
        {           
            // Build DbContextOptions
            _dbContextOptions = new DbContextOptionsBuilder<ProjectContext>()
                .UseInMemoryDatabase(databaseName: "TestProject")
                .Options;
            _context = new ProjectContext(_dbContextOptions);
            _repository = new Repository<User>(_context);
            _userRepository = new UserRepository(_context);

            _passwordHasher = new PasswordHasher();

            Environment.SetEnvironmentVariable("JWT_SECRET", "this is my custom Secret key for authentication");
            Environment.SetEnvironmentVariable("JWT_ISSUER", "PROJECT");

        }

        [Test]
        public async Task CreateUser_UserNotExists_ReturnNewUserAsync()
        {
            // Arrange
            var name = "André";
            var email = "andre@gmail.com";

            var command = new CreateUserCommand(name, email, "1234", Roles.Administrator);
            var handler = new CreateUserHandler(_repository, _userRepository, _passwordHasher);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.Id, Is.TypeOf<Guid>());
            Assert.That(result.Name, Is.EqualTo(name));
            Assert.That(result.Name, Is.EqualTo(_context.Users.First().Name));
            Assert.That(result.Id, Is.EqualTo(_context.Users.First().Id));

        }

        [Test]
        public void CreateUser_UserExists_ThrowApplicationDataExeption()
        {
            var name = "João";
            var email = "joao@gmail.com";

            //_userRepository.Setup(repo => repo.GetByEmail(email)).ReturnsAsync(
            //    new User("André", "andre@gmail.com", "1234", "Users"));
            
            _context.Users.Add(new User("João", "joao@gmail.com", "1234", Roles.Administrator));
            _context.SaveChanges();

            var command = new CreateUserCommand(name, email, "1234", Roles.Administrator);
            var handler = new CreateUserHandler(_repository, _userRepository, _passwordHasher);


            Assert.That(() => handler.Handle(command, CancellationToken.None), Throws.Exception.TypeOf<ApplicationDataException>());

        }

        [Test]
        public void CreateUser_InvalidCommands_ReturnFalse()
        {
            //_userRepository.Setup(repo => repo.GetByEmail("andre@gmail.com")).ReturnsAsync((User)null);

            var command = new CreateUserCommand("", "teste@gmail.com", "1234", Roles.Administrator);
            var handler = new CreateUserHandler(_repository, _userRepository, _passwordHasher);

            Assert.IsFalse(command.Validate().IsValid);
            Assert.That(() => handler.Handle(command, CancellationToken.None), Throws.Exception.TypeOf<ApplicationDataException>());

        }
    }
}
