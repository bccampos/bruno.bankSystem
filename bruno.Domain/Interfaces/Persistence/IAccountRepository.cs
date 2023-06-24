using bruno.BankSystem.Domain.Entities;

namespace bruno.BankSystem.Domain.Interfaces.Persistence
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAllAsync();

        Task<Account> GetByIdAsync(string accountNumber);

        void Add(Account account);

        void Delete(Account account);

        Task CommitAsync();
    }
}
