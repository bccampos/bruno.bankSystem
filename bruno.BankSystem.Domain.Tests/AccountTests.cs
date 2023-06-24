using bruno.BankSystem.Domain.Common.Enums;
using bruno.BankSystem.Domain.Entities;
using bruno.BankSystem.Domain.Tests.Helpers;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace bruno.BankSystem.Domain.Tests
{
    public class AccountTests
    {
        private readonly string _accountNumber;
        private readonly decimal _balance;
        private readonly UserId _userId;

        public AccountTests()
        {
            _accountNumber = "123";
            _balance = 150;
            _userId = UserId.Parse(Guid.NewGuid());
        }


        [Fact]
        public void InstantiateAccount_WithValidData_ShouldCreateWithSuccess()
        {
            // Arrange, Act
            var account = new Account().Create(_accountNumber, _balance, _userId);

            // Assert
            Assert.Equal(_accountNumber, account.AccountNumber);
            Assert.Equal(_balance, account.Balance);
            Assert.Equal(_userId, account.UserId);
            Assert.Equal(Status.Active, account.Status);
        }

        [Fact]
        public void InstantiateAccount_WithInvalidData_ShouldThrowException()
        {
            // Arrange, Act
            Action actInvalidId = () => new Account().Create(_accountNumber, _balance, UserId.Parse(Guid.Empty)); 
            Action actInvalidAccountNumber = () => new Account().Create(string.Empty, _balance, UserId.Parse(Guid.NewGuid()));
            Action actInvalidBalance = () => new Account().Create(_accountNumber, 90, UserId.Parse(Guid.NewGuid()));

            // Assert
            AssertionHelper.AssertDomainException(Record.Exception(actInvalidId), "The field UserId can't be empty");
            AssertionHelper.AssertDomainException(Record.Exception(actInvalidAccountNumber), "The field AccountNumber can't be empty");
            AssertionHelper.AssertDomainException(Record.Exception(actInvalidBalance), "Initial balance should be at least $100");
        }

        [Fact]
        public void ActivateAccount_ShouldActivateWithSuccess()
        {
            var account = new Account().Create(_accountNumber, _balance, _userId);

            account.Activate();

            Assert.Equal(Status.Active, account.Status);
        }

        [Fact]
        public void InactivateProduct_ShouldInactivateWithSuccess()
        {
            var account = new Account().Create(_accountNumber, _balance, _userId);

            account.Inactivate();

            Assert.Equal(Status.Inactive, account.Status);
        }
    }
}