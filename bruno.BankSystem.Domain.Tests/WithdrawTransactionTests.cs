using bruno.BankSystem.Domain.Entities;
using bruno.BankSystem.Domain.Tests.Helpers;

namespace bruno.BankSystem.Domain.Tests
{
    public class WithdrawTransactionTests
    {
        private readonly string _accountNumber;
        private readonly UserId _userId;

        public WithdrawTransactionTests()
        {
            _accountNumber = "123";
            _userId = UserId.Parse(Guid.NewGuid());
        }

        [Fact]
        public void WithdrawTransaction_WithMoreThan90Percent_ShouldThrowException()
        {
            // Arrange, Act
            var account = new Account().Create(_accountNumber, 200, _userId);

            Action actInvalidBalance = () => account.DebitTransaction(190);

            // Assert
            AssertionHelper.AssertDomainException(Record.Exception(actInvalidBalance), "Withdraw transaction amount is greater than 90% of balance");
        }

        [Fact]
        public void WithdrawTransaction_WithInsufficientBalance_ShouldThrowException()
        {
            // Arrange, Act
            var account = new Account().Create(_accountNumber, 100, _userId);

            Action actInvalidBalance = () => account.DebitTransaction(10);

            // Assert
            AssertionHelper.AssertDomainException(Record.Exception(actInvalidBalance), "Withdraw transaction insufficient balance");
        }

        [Fact]
        public void WithdrawTransaction_WithValidData_ShouldDebitWithSuccess()
        {
            // Arrange, Act
            var account = new Account().Create(_accountNumber, 500, _userId);

            account.DebitTransaction(100);

            // Assert
            Assert.Equal(500 - 100, account.Balance);
        }
    }
}