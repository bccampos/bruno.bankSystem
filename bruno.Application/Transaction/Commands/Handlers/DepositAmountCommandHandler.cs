using bruno.BankSystem.Application.Common.Errors;
using bruno.BankSystem.Domain.Interfaces.Persistence;
using bruno.BankSystem.Domain.Interfaces.UnitOfWork;
using FluentResults;
using MediatR;

namespace bruno.BankSystem.Application.Transaction.Commands.Handlers
{
    public class DepositAmountCommandHandler : IRequestHandler<DepositAmountCommand, Result<decimal>>
    {
        private readonly IAccountRepository _accountRepository;

        public DepositAmountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Result<decimal>> Handle(DepositAmountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsync(request.AccountNumber);
            if (account is null)
            {
                return Result.Fail($"Account {request.AccountNumber} does not exist");
            }

            if (!account.HasMaxDepositTransactionAllowed(request.Amount))
            {
                return Result.Fail($"Deposit Transaction maximum limit exceeded ${request.Amount}");
            }

            account.DepositTransaction(request.Amount);

            _accountRepository.Add(account);

            await _accountRepository.CommitAsync();

            return Result.Ok<decimal>(account.Balance);
        }
    }
}
