using bruno.WebApi.Filter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace bruno.WebApi.Controllers
{
    [Route("user")]
    [ApiController]
    [ErrorHandlingFilter]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //TODO: Add methods to Get and Create User



        //[HttpPost("register")]
        //public async Task<IActionResult> Register(RegisterRequest request)
        //{
        //    var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);

        //    Result<AuthenticationResult> authResult = await _mediator.Send(command);

        //    if (authResult.IsSuccess)
        //    {
        //        return Ok(MapAuthResult(authResult.Value));
        //    }

        //    var firstError = authResult.Errors[0];
        //    if (firstError is DuplicateEmailError) 
        //    {
        //        return Problem(statusCode: StatusCodes.Status409Conflict, detail: firstError.Message);
        //    }

        //    return Problem($"Unexpected error: {firstError.Message}");
        //}

        //[HttpPost("login")]
        //public async Task<IActionResult> Login(LoginRequest request)
        //{
        //    var query = new LoginQuery(request.Email, request.Password);

        //    var authResult = await _mediator.Send(query);

        //    if (authResult.IsSuccess)
        //    {
        //        return Ok(MapAuthResult(authResult.Value));
        //    }

        //    var firstError = authResult.Errors[0];
        //    if (firstError is DuplicateEmailError)
        //    {
        //        return Problem(statusCode: StatusCodes.Status409Conflict, detail: firstError.Message);
        //    }

        //    return Problem($"Unexpected error: {firstError.Message}");
        //}

        //private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
        //{ 
        //    return new AuthenticationResponse(
        //        authResult.user.Id,
        //        authResult.user.FirstName,
        //        authResult.user.LastName,
        //        authResult.user.Email,
        //        authResult.Token);
        //}
    }
}
