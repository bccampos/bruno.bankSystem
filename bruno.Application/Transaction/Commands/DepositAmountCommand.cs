using FluentResults;
using MediatR;

namespace bruno.BankSystem.Application.Transaction.Commands
{
    public record DepositAmountCommand(
         string AccountNumber,
         decimal Amount
         ) : IRequest<Result<decimal>>;
}
