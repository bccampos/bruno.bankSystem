using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bruno.BankSystem.Contracts.Account
{
    public record AccountRequest(
    string UserPrefix,
    string AccountNumber,
    decimal Balance);
}
