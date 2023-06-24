using bruno.BankSystem.Domain.Exceptions;
using FluentResults;

namespace bruno.BankSystem.Application.Common.Errors
{
    public class NotFoundError : IError
    {
        public List<IError> Reasons => throw new NotFoundException("item does not exist");

        public Dictionary<string, object> Metadata => throw new NotImplementedException();

        public string Message => "item does not exist";
    }
}
