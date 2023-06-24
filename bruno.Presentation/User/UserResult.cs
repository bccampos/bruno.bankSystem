using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bruno.BankSystem.Contracts.User
{
    public record UserResult(
         Guid Id,
         string UserId,
         string FirstName,
         string LastName,
         string Email);
}
