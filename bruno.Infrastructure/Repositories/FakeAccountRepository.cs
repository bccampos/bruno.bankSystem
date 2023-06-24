using bruno.BankSystem.Domain;
using bruno.BankSystem.Domain.Common.Enums;
using bruno.BankSystem.Domain.Entities;
using bruno.BankSystem.Domain.Interfaces.Persistence;

namespace bruno.BankSystem.Infrastructure.Repositories
{
    public class FakeAccountRepository : IAccountRepository
    {
        private readonly static List<Account> _existing = GetFakeAccounts();

        public int CommitCalledCount { get; set; }
        public int DeleteCalledCount { get; set; }

        public FakeAccountRepository()
        {
        }

        public Task<List<Account>> GetAllAsync()
        {
            return Task.FromResult(_existing);
        }

        public Task<Account> GetByIdAsync(string accountNumber)
        {
            return Task.FromResult(_existing.Find(e => e.AccountNumber == accountNumber && e.Status == Status.Active));
        }

        public void Add(Account account)
        {
            _existing.Add(account);
        }

        public void Delete(Account account)
        {
            _existing.Remove(account);
        }

        public Task CommitAsync()
        {
            CommitCalledCount++;
            return Task.CompletedTask;
        }

        private static List<Account> GetFakeAccounts()
        {
            return new List<Account>()
            {
                new Account().Create("123", 500, UserId.Parse(Guid.NewGuid())),
                new Account().Create("1234", 100, UserId.Parse(Guid.NewGuid()))
            };
        }
    }
}
