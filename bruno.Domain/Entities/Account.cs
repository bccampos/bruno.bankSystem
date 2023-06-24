using bruno.BankSystem.Domain.Common.Enums;
using bruno.BankSystem.Domain.Models;
using bruno.Domain.Entities;
using bruno.BankSystem.Domain.Core;
using System.Security.Principal;
using bruno.BankSystem.Domain.Exceptions;
using System.Runtime.CompilerServices;

namespace bruno.BankSystem.Domain.Entities
{
    public class Account : Entity<AccountId>
    {
        private const int MinAccountBalance = 100;
        private const int MaxWithdrawPercentage = 90;
        private const int MaxDepositTransaction = 10000;

        public string AccountNumber { get; private set; }
        public decimal Balance { get; private set; }
        public Status Status { get; private set; }

        public UserId UserId { get; private set; }
        public virtual User User { get; private set; }

        public Account()
        {

        }

        private Account(AccountId id, string accountNumber, decimal balance, UserId userId) : base(id)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            UserId = userId;

            Validate();
        }

        public Account Create(string accountNumber, decimal balance, UserId userId)
        {
            Activate();

            return new Account(AccountId.Create(), accountNumber, balance, userId);
        }

        public bool HasMinimumAmount(decimal withdrawRequest)
        { 
            return (Balance - withdrawRequest) > MinAccountBalance;
        }

        public bool HasMaxWithdrawPercentageAllowed(decimal withdrawRequest)
        {
            return withdrawRequest < (Balance * MaxWithdrawPercentage / 100);
        }

        public bool HasMaxDepositTransactionAllowed(decimal withdrawRequest) 
        {
            return withdrawRequest < MaxDepositTransaction;
        }

        public void DebitTransaction(decimal withdrawRequest)
        {
            if (!HasMaxWithdrawPercentageAllowed(withdrawRequest))
            {
                throw new DomainException("Withdraw transaction amount is greater than 90% of balance");
            }

            if (!HasMinimumAmount(withdrawRequest))
            {
                throw new DomainException("Withdraw transaction insufficient balance");
            }         

            Balance = Balance - withdrawRequest;
        }

        public void DepositTransaction(decimal depositRequest)
        {
            if (!HasMaxDepositTransactionAllowed(depositRequest))
            {
                throw new DomainException("Deposit Transaction maximum limit exceeded");
            }

            Balance = Balance + depositRequest;
        }

        public void Activate() => Status = Status.Active;

        public void Inactivate() => Status = Status.Inactive;

        public bool Equals(Account other)
        {
            return other != null && other.Id == this.Id;
        }

        public void Validate()
        {
            Validation.ValidateIfEquals(Id, Guid.Empty, $"The field Id can't be empty");
            Validation.ValidateIfEmpty(AccountNumber, $"The field AccountNumber can't be empty");
            Validation.ValidateIfLesserThan(Balance, MinAccountBalance, $"Initial balance should be at least $100");
            Validation.ValidateIfEquals(UserId.Value, Guid.Empty, $"The field UserId can't be empty");
        }
    }
}
