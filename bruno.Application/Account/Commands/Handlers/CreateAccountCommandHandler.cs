using bruno.BankSystem.Application.Common.Errors;
using bruno.BankSystem.Domain.Interfaces.Persistence;
using bruno.Domain.Entities;
using FluentResults;
using MediatR;

namespace bruno.BankSystem.Application.Account.Commands.Handlers
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Result>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;

        public CreateAccountCommandHandler(IAccountRepository accountRepository, IUserRepository userRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
        }


        public async Task<Result> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByIdAsync(request.UserPrefix);

            if (user is null)
            {
                return Result.Fail($"User {request.UserPrefix} does not exist");
            }
                       
            var account = new Domain.Entities.Account().Create(request.AccountNumber, request.Balance, user.Id);

            _accountRepository.Add(account);

            await _accountRepository.CommitAsync();

            return Result.Ok();
        }
    }
}
