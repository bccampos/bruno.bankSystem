using bruno.BankSystem.Application.Account.Commands;
using bruno.BankSystem.Application.Account.Commands.Handlers;
using bruno.BankSystem.Application.Common.Errors;
using bruno.BankSystem.Domain.Entities;
using bruno.BankSystem.Domain.Interfaces.Persistence;
using bruno.Domain.Entities;
using FluentAssertions;
using FluentResults;
using Moq;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace bruno.BankSystem.Applications.Tests
{
    public class CreateAccountCommandHandlerTests
    {
        private readonly IAccountRepository _accountRepository;

        public CreateAccountCommandHandlerTests()
        {
            _accountRepository = Substitute.For<IAccountRepository>();
        }

        [Fact]
        public async Task ShouldCreateAccountWithSuccess()
        {
            var user = new User().Create("test", "bruno", "campos", string.Empty);

            Moq.Mock<IUserRepository> mock = new Moq.Mock<IUserRepository>();
            mock.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            var command = new CreateAccountCommand(user.UserPrefix, "123", 150);

            var handler = new CreateAccountCommandHandler(_accountRepository, mock.Object);

            Result response = await handler.Handle(command, CancellationToken.None);

            response.Should().NotBeNull();
            response.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task IfUserDoesNotExist_ShouldThrowException_NotFound()
        {
            var user = new User().Create(string.Empty, "bruno", "campos", string.Empty);

            Moq.Mock<IUserRepository> mock = new Moq.Mock<IUserRepository>();
            mock.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(value: null);

            var command = new CreateAccountCommand(user.UserPrefix, "123", 150);

            var handler = new CreateAccountCommandHandler(_accountRepository, mock.Object);

            Result response = await handler.Handle(command, CancellationToken.None);

            response.Should().NotBeNull();
            response.IsSuccess.Should().BeFalse();
            response.IsFailed.Should().BeTrue();

            Assert.IsType<NotFoundError>(response.Reasons.First());

        }
    }
}