using bruno.BankSystem.Contracts.User;

namespace bruno.BankSystem.Contracts.Account
{
    public record AccountResult(
    Guid Id,
    string AccountNumber,
    decimal Balance,
    Guid UserId);
}
