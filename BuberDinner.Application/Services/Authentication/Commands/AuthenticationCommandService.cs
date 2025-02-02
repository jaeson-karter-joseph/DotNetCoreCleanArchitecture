using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Domain.Entites;
using BuberDinner.Domain.Errors;
using ErrorOr;


namespace BuberDinner.Application.Services.Authentication.Commands
{
    public class AuthenticationCommandService(IJwtTokenGenerator _jwtTokenGenerator, IUserRespository _userRespository) : IAuthenticationCommandService
    {
        public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        {
            //1. Validate the does not exsits
            if (_userRespository.GetUserByEmail(email) is not null)
            {
                return Errors.User.DuplicateEmail;
            }

            //Create User (Generate unique ID)
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            _userRespository.AddUser(user);

            //Generate JWT
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
