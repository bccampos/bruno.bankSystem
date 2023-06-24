using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bruno.BankSystem.Application.Transaction.Commands
{
    public record WithdrawAmountCommand(
         string AccountNumber,
         decimal Amount
         ) : IRequest<Result<decimal>>;
}
