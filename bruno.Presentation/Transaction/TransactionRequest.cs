using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bruno.BankSystem.Contracts.Transaction
{
    public record TransactionRequest(
    string AccountNumber,
    decimal Amount);
}
