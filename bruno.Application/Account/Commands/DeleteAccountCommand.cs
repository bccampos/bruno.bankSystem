using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bruno.BankSystem.Application.Account.Commands
{
    public record DeleteAccountCommand(
     string AccountNumber
     ) : IRequest<Result>;
}
