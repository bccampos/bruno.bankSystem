using bruno.BankSystem.Domain;
using bruno.BankSystem.Domain.Models;

namespace bruno.Domain.Entities
{
    public class User : Entity<UserId>
    {
        public string UserPrefix { get; set; }  

        public string FirstName { get; private set; }

        public string LastName { get; private set; } 

        public string Email { get; private set; }

        public User() { }

        private User(UserId id, string userPrefix, string firstName, string lastName, string email) : base(id)
        {
            UserPrefix = userPrefix;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public User Create(string userPrefix, string firstName, string lastName, string email)
        {
            return new User(UserId.Create(), userPrefix, firstName, lastName, email);
        }

        public void Update(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

    }
}
