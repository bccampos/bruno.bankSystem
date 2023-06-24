using bruno.BankSystem.Contracts.Account;
using bruno.BankSystem.Domain.Interfaces.Persistence;
using FluentResults;
using MediatR;

namespace bruno.BankSystem.Application.Account.Queries.Handlers
{
    public class GetAccountListQueryHandler : IRequestHandler<GetAccountListQuery, Result<List<AccountResult>>>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAccountListQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Result<List<AccountResult>>> Handle(GetAccountListQuery request, CancellationToken cancellationToken)
        {
            var result = await _accountRepository.GetAllAsync();

            return Result.Ok<List<AccountResult>>(result.Select(x => new AccountResult
                    (x.Id.Value, x.AccountNumber, x.Balance, x.UserId.Value)).ToList());
        }
    }
}
