using bruno.BankSystem.Domain.Entities;
using bruno.BankSystem.Domain.Tests.Helpers;

namespace bruno.BankSystem.Domain.Tests
{
    public class DepositTransactionTests
    {
        private readonly string _accountNumber;
        private readonly UserId _userId;

        public DepositTransactionTests()
        {
            _accountNumber = "123";
            _userId = UserId.Parse(Guid.NewGuid());
        }

        [Fact]
        public void DepositTransaction_WithMaxDepositTransactionAllowed_ShouldThrowException()
        {
            // Arrange, Act
            var account = new Account().Create(_accountNumber, 200, _userId);

            Action actInvalidBalance = () => account.DepositTransaction(10000);

            // Assert
            AssertionHelper.AssertDomainException(Record.Exception(actInvalidBalance), "Deposit Transaction maximum limit exceeded");
        }

        [Fact]
        public void DepositTransaction_WithValidData_ShouldDebitWithSuccess()
        {
            // Arrange, Act
            var account = new Account().Create(_accountNumber, 500, _userId);

            account.DepositTransaction(100);

            // Assert
            Assert.Equal(500 + 100, account.Balance);
        }
    }
}