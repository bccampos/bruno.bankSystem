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
    public class DeleteAccountCommandHandlerTests
    {
        [Fact]
        public async Task ShouldDeleteAccountWithSuccess()
        {
            var user = new User().Create("test", "bruno", "campos", string.Empty);
            var account = new Account().Create("123", 150, user.Id);

            Moq.Mock<IAccountRepository> mock = new Moq.Mock<IAccountRepository>();
            mock.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(account);

            var command = new DeleteAccountCommand(account.AccountNumber);

            var handler = new DeleteAccountCommandHandler(mock.Object);

            Result response = await handler.Handle(command, CancellationToken.None);

            response.Should().NotBeNull();
            response.IsSuccess.Should().BeTrue();
        }


    }
}