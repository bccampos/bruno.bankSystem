using bruno.BankSystem.Domain;
using bruno.BankSystem.Domain.Interfaces.Persistence;
using bruno.Domain.Entities;

namespace bruno.BankSystem.Infrastructure.Repositories
{
    public class FakeUserRepository : IUserRepository
    {
        private readonly static List<User> _existing = GetFakeUsers();

        public int CommitCalledCount { get; set; }
        public int DeleteCalledCount { get; set; }

        public FakeUserRepository()
        {
        }

        public Task<List<User>> GetAllAsync()
        {
            return Task.FromResult(_existing);
        }

        public Task<User> GetByIdAsync(string userPrefix)
        {
            return Task.FromResult(_existing.Find(e => e.UserPrefix == userPrefix));
        }

        public void Add(User user)
        {
            _existing.Add(user);
        }

        public void Delete(User user)
        {
            _existing.Remove(user);
        }

        public Task CommitAsync()
        {
            CommitCalledCount++;
            return Task.CompletedTask;
        }

        private static List<User> GetFakeUsers()
        {
            return new List<User>()
            {
                new User().Create("1", "Bruno", "Campos", ""),
                new User().Create("2", "John", "Lennon", ""),
            };
        }
    }
}
