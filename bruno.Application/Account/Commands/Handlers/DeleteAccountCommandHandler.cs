using bruno.BankSystem.Domain.Interfaces.Persistence;
using FluentResults;
using MediatR;

namespace bruno.BankSystem.Application.Account.Commands.Handlers
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Result>
    {
        private readonly IAccountRepository _accountRepository;

        public DeleteAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Result> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsync(request.AccountNumber);

            account.Inactivate();

            await _accountRepository.CommitAsync();

            return Result.Ok();
        }
    }
}
