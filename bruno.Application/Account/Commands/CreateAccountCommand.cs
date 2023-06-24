using FluentResults;
using MediatR;

namespace bruno.BankSystem.Application.Account.Commands
{
    public record CreateAccountCommand(
     string UserPrefix, //or we use here UserId (UserPrefix will make my life easier in this case)
     string AccountNumber,
     decimal Balance
     ) : IRequest<Result>;
}
