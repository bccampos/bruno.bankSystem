using bruno.BankSystem.Application.Account.Commands;
using bruno.BankSystem.Application.Transaction.Commands;
using bruno.BankSystem.Contracts.Account;
using bruno.BankSystem.Contracts.Transaction;
using bruno.WebApi.Filter;
using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace bruno.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ErrorHandlingFilter]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TransactionController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] TransactionRequest request)
        {
            if (string.IsNullOrEmpty(request.AccountNumber))
            {
                return BadRequest();
            }

            if (request.Amount <= 0)
            {
                return BadRequest();
            }

            var command = _mapper.Map<WithdrawAmountCommand>(request);

            Result<decimal> result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok($"Balance result {result.Value}");
            }

            return Problem($"{result.Errors[0]}");
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] TransactionRequest request)
        {
            if (string.IsNullOrEmpty(request.AccountNumber))
            {
                return BadRequest();
            }

            if (request.Amount <= 0)
            {
                return BadRequest();
            }

            var command = _mapper.Map<DepositAmountCommand>(request);

            Result<decimal> result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok($"Balance result {result.Value}");
            }

            return Problem($"{result.Errors[0]}");
        }
    }
}
