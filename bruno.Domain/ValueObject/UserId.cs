using bruno.BankSystem.Domain.Models;

namespace bruno.BankSystem.Domain
{
    public class UserId : ValueObject
    {
        public Guid Value { get; }

        private UserId()
        {
        }

        private UserId(Guid value)
        {
            Value = value;
        }

        public static UserId Create()
        {
            return new(Guid.NewGuid());
        }

        public static UserId Create(Guid Value)
        {
            return new UserId(Value);
        }

        public static UserId Parse(Guid value)
        {
            return new UserId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
