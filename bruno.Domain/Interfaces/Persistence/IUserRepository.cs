using bruno.BankSystem.Domain.Entities;
using bruno.Domain.Entities;

namespace bruno.BankSystem.Domain.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();

        Task<User> GetByIdAsync(string userNumber);

        void Add(User account);

        void Delete(User account);

        Task CommitAsync();
    }
}
