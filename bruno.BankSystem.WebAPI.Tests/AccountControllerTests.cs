using bruno.BankSystem.Application.Account.Commands;
using bruno.BankSystem.Contracts.Account;
using bruno.WebApi.Controllers;
using FluentResults;
using MapsterMapper;
using MediatR;
using Moq;
using FluentAssertions;

namespace bruno.BankSystem.WebAPI.Tests
{
    public class AccountControllerTests
    {
        [Fact]
        public async Task CreateAccountController_IfValidDataShouldReturnSucess()
        {
            Mock<IMediator> mock = new Mock<IMediator>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            var setup = new CreateAccountCommand("test", "123", 150);
            var command = new AccountResult(Guid.NewGuid(), setup.AccountNumber, setup.Balance, Guid.NewGuid());

            mockMapper.Setup(x => x.Map<CreateAccountCommand>(It.IsAny<AccountRequest>()))
                .Returns(setup);

            mock.Setup(m => m.Send(It.IsAny<CreateAccountCommand>(), default))
                .ReturnsAsync(Result.Ok);

            var controller = new AccountController(mock.Object, mockMapper.Object);

            var result = await controller.Create(new AccountRequest("test", "123", 150));

            result.Should().NotBeNull();
        }
    }
}