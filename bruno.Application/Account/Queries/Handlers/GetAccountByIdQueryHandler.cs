using bruno.BankSystem.Application.Common.Errors;
using bruno.BankSystem.Contracts.Account;
using bruno.BankSystem.Domain.Interfaces.Persistence;
using FluentResults;
using MediatR;

namespace bruno.BankSystem.Application.Account.Queries.Handlers
{
    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, Result<AccountResult>>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAccountByIdQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Result<AccountResult>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _accountRepository.GetByIdAsync(request.AccountNumber);

            if (result is null)
            {
                return Result.Fail($"Account {request.AccountNumber} does not exist");
            }

            return new AccountResult(result.Id.Value, result.AccountNumber, result.Balance, result.UserId.Value);
        }
    }
}
