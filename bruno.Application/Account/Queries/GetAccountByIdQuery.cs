using bruno.BankSystem.Contracts.Account;
using FluentResults;
using MediatR;

namespace bruno.BankSystem.Application.Account.Queries
{
    public record GetAccountByIdQuery(
     string AccountNumber
     ) : IRequest<Result<AccountResult>>;

}
