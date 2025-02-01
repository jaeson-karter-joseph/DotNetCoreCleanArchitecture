using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            Result<AuthenticationResult> regitserResult = authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);

            if (regitserResult.IsSuccess)
            {
                return Ok(MapAuthResult(regitserResult.Value));
            }

            var firstError = regitserResult.Errors[0];
            if (firstError is DuplicateEmailError)
            {
                return Problem(statusCode: StatusCodes.Status409Conflict, title: "Email Already Exists.");
            }

            return Problem();

        }


        private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
        {
            return new AuthenticationResponse(authResult.User.Id, authResult.User.FirstName, authResult.User.LastName, authResult.User.Email, authResult.Token);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var authResult = authenticationService.Login(request.Email, request.Password);
            var response = new AuthenticationResponse(authResult.User.Id, authResult.User.FirstName, authResult.User.LastName, authResult.User.Email, authResult.Token);
            return Ok(response);
        }
    }
}
