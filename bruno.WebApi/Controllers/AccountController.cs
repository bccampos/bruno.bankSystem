using bruno.BankSystem.Application.Account.Commands;
using bruno.BankSystem.Application.Account.Queries;
using bruno.BankSystem.Application.Common.Errors;
using bruno.BankSystem.Contracts.Account;
using bruno.BankSystem.Domain.Exceptions;
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
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AccountController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] AccountRequest request)
        {
            var command = _mapper.Map<CreateAccountCommand>(request);

            Result<AccountResult> result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(_mapper.Map<AccountResult>(result.Value));
            }

            var firstError = result.Errors[0];
            if (firstError is NotFoundError)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: firstError.Message);
            }

            return Problem($"Unexpected error: {result.Errors[0]}");
        }

        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string accountNumber)
        {
            try
            {
                await _mediator.Send(new DeleteAccountCommand(accountNumber));

                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("getById/{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] string accountNumber)
        {
            var query = new GetAccountByIdQuery(accountNumber);

            var response = await _mediator.Send(query);

            if (response.IsSuccess)
            {
                return Ok(response.Value);
            }

            //var firstError = response.Errors[0];
            //if (firstError is NotFoundError)
            //{
            //    return Problem(statusCode: StatusCodes.Status409Conflict, detail: firstError.Message);
            //}

            return Problem($"Unexpected error: ");

        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAccountListQuery();

            var response = await _mediator.Send(query);

            return Ok(response.Value);
        }
    }
}
