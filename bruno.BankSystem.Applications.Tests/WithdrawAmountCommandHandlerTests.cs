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
    public class WithdrawAmountCommandHandlerTests
    {
        private readonly IAccountRepository _accountRepository;

        public WithdrawAmountCommandHandlerTests()
        {
            _accountRepository = Substitute.For<IAccountRepository>();
        }

        [Fact]
        public async Task IfAccountDoesNotExist_ShouldThrowException_NotFound()
        {
            Moq.Mock<IAccountRepository> mock = new Moq.Mock<IAccountRepository>();
            mock.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(value: null);

            var command = new WithdrawAmountCommand("123", 150);

            var handler = new WithdrawAmountCommandHandler(_accountRepository);

            Result<decimal> response = await handler.Handle(command, CancellationToken.None);

            response.Should().NotBeNull();
            response.IsSuccess.Should().BeFalse();
            response.IsFailed.Should().BeTrue();

            response.Errors[0].Message.Should().Be("Account 123 does not exist");
        }

        [Fact]
        public async Task WithdrawTransaction_WithNotMinimumAmount_ShouldThrowException()
        {
            var account = new Account().Create("123", 100, UserId.Parse(Guid.NewGuid()));

            Moq.Mock<IAccountRepository> mock = new Moq.Mock<IAccountRepository>();
            mock.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(account);

            var command = new WithdrawAmountCommand("123", 10);

            var handler = new WithdrawAmountCommandHandler(mock.Object);

            Result<decimal> response = await handler.Handle(command, CancellationToken.None);

            response.Should().NotBeNull();
            response.IsSuccess.Should().BeFalse();
            response.IsFailed.Should().BeTrue();

            response.Errors[0].Message.Should().Be("Withdraw transaction for insufficient balance $100");
        }

        [Fact]
        public async Task WithdrawTransaction_WithWithdrawPercentageNotAllowed_ShouldThrowException()
        {
            var account = new Account().Create("123", 300, UserId.Parse(Guid.NewGuid()));

            Moq.Mock<IAccountRepository> mock = new Moq.Mock<IAccountRepository>();
            mock.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(account);

            var command = new WithdrawAmountCommand("123", 280);

            var handler = new WithdrawAmountCommandHandler(mock.Object);

            Result<decimal> response = await handler.Handle(command, CancellationToken.None);

            response.Should().NotBeNull();
            response.IsSuccess.Should().BeFalse();
            response.IsFailed.Should().BeTrue();

            response.Errors[0].Message.Should().Be("Withdraw transaction request is greater than 90% of balance $300");
        }


        [Fact]
        public async Task ShouldWithdrawAmountWithSuccess()
        {
            var account = new Account().Create("123", 300, UserId.Parse(Guid.NewGuid()));

            Moq.Mock<IAccountRepository> mock = new Moq.Mock<IAccountRepository>();
            mock.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(account);

            var command = new WithdrawAmountCommand("123", 80);

            var handler = new WithdrawAmountCommandHandler(mock.Object);

            Result<decimal> response = await handler.Handle(command, CancellationToken.None);

            response.Should().NotBeNull();
            response.IsSuccess.Should().BeTrue();

            response.Value.Should().Be(220);
        }
    }
}