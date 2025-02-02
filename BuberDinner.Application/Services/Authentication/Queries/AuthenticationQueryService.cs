using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Domain.Entites;
using BuberDinner.Domain.Errors;
using ErrorOr;


namespace BuberDinner.Application.Services.Authentication
{
    public class AuthenticationQueryService(IJwtTokenGenerator _jwtTokenGenerator, IUserRespository _userRespository) : IAuthenticationQueryService
    {
        public ErrorOr<AuthenticationResult> Login(string email, string password)
        {
            //Validate User Does Exist
            if (_userRespository.GetUserByEmail(email) is not User user)
            {
                return Errors.Authentication.InvalidEmail;
            }

            //Validate Password
            if (user.Password != password)
            {
                return new[] { Errors.Authentication.InvalidPassword, Errors.Authentication.InvalidEmail };
            }

            var token = _jwtTokenGenerator.GenerateToken(user);


            return new AuthenticationResult(user, token);

        }
    }
}
