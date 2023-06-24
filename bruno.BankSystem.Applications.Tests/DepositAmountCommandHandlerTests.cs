using bruno.BankSystem.Application.Transaction.Commands;
using bruno.BankSystem.Application.Transaction.Commands.Handlers;
using bruno.BankSystem.Domain;
using bruno.BankSystem.Domain.Entities;
using bruno.BankSystem.Domain.Interfaces.Persistence;
using FluentAssertions;
using FluentResults;
using Moq;
using NSubstitute;

namespace bruno.BankSystem.Applications.Tests
{
    public class DepositAmountCommandHandlerTests
    {
        private readonly IAccountRepository _accountRepository;

        public DepositAmountCommandHandlerTests()
        {
            _accountRepository = Substitute.For<IAccountRepository>();
        }

        [Fact]
        public async Task IfAccountDoesNotExist_ShouldThrowException_NotFound()
        {
            Moq.Mock<IAccountRepository> mock = new Moq.Mock<IAccountRepository>();
            mock.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(value: null);

            var command = new DepositAmountCommand("123", 150);

            var handler = new DepositAmountCommandHandler(_accountRepository);

            Result<decimal> response = await handler.Handle(command, CancellationToken.None);

            response.Should().NotBeNull();
            response.IsSuccess.Should().BeFalse();
            response.IsFailed.Should().BeTrue();

            response.Errors[0].Message.Should().Be("Account 123 does not exist");
        }

        [Fact]
        public async Task DepositTransaction_WithMaxDepositTransactionNotAllowed_ShouldThrowException()
        {
            var account = new Account().Create("123", 100, UserId.Parse(Guid.NewGuid()));

            Moq.Mock<IAccountRepository> mock = new Moq.Mock<IAccountRepository>();
            mock.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(account);

            var command = new DepositAmountCommand("123", 10000);

            var handler = new DepositAmountCommandHandler(mock.Object);

            Result<decimal> response = await handler.Handle(command, CancellationToken.None);

            response.Should().NotBeNull();
            response.IsSuccess.Should().BeFalse();
            response.IsFailed.Should().BeTrue();

            response.Errors[0].Message.Should().Be("Deposit Transaction maximum limit exceeded $10000");
        }

        [Fact]
        public async Task ShouldDepositAmountWithSuccess()
        {
            var account = new Account().Create("123", 300, UserId.Parse(Guid.NewGuid()));

            Moq.Mock<IAccountRepository> mock = new Moq.Mock<IAccountRepository>();
            mock.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(account);

            var command = new DepositAmountCommand("123", 80);

            var handler = new DepositAmountCommandHandler(mock.Object);

            Result<decimal> response = await handler.Handle(command, CancellationToken.None);

            response.Should().NotBeNull();
            response.IsSuccess.Should().BeTrue();

            response.Value.Should().Be(380);
        }
    }
}