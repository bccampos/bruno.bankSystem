using bruno.BankSystem.Contracts.Account;
using FluentResults;
using MediatR;

namespace bruno.BankSystem.Application.Account.Queries
{
    public record GetAccountListQuery : IRequest<Result<List<AccountResult>>>;
}
