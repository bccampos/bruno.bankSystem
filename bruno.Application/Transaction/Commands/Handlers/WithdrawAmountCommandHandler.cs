using bruno.BankSystem.Application.Common.Errors;
using bruno.BankSystem.Domain.Interfaces.Persistence;
using FluentResults;
using MediatR;

namespace bruno.BankSystem.Application.Transaction.Commands.Handlers
{
    public class WithdrawAmountCommandHandler : IRequestHandler<WithdrawAmountCommand, Result<decimal>>
    {
        private readonly IAccountRepository _accountRepository;

        public WithdrawAmountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }


        public async Task<Result<decimal>> Handle(WithdrawAmountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsync(request.AccountNumber);
            if (account is null)
            {
                return Result.Fail($"Account {request.AccountNumber} does not exist");
            }

            if (!account.HasMaxWithdrawPercentageAllowed(request.Amount))
            {
                return Result.Fail($"Withdraw transaction request is greater than 90% of balance ${account.Balance}");
            }

            if (!account.HasMinimumAmount(request.Amount))
            {
                return Result.Fail($"Withdraw transaction for insufficient balance ${account.Balance}");
            }      

            account.DebitTransaction(request.Amount);

            _accountRepository.Add(account);

            await _accountRepository.CommitAsync();

            return Result.Ok<decimal>(account.Balance);
        }
    }
}
