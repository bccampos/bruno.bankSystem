using bruno.BankSystem.Domain.Models;

namespace bruno.BankSystem.Domain
{
    public class AccountId : ValueObject
    {
        public Guid Value { get; }

        private AccountId()
        {
        }

        private AccountId(Guid value)
        {
            Value = value;
        }

        public static AccountId Create()
        {
            return new(Guid.NewGuid());
        }

        public static AccountId Create(Guid Value)
        {
            return new AccountId(Value);
        }

        public static AccountId Parse(Guid value)
        {
            return new AccountId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
