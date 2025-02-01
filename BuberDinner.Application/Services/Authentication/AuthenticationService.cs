using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entites;
using FluentResults;


namespace BuberDinner.Application.Services.Authentication
{
    public class AuthenticationService(IJwtTokenGenerator _jwtTokenGenerator, IUserRespository _userRespository) : IAuthenticationService
    {
        public Result<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        {
            //1. Validate the does not exsits
            if (_userRespository.GetUserByEmail(email) is not null)
            {
                return Result.Fail<AuthenticationResult>(new[] { new DuplicateEmailError() });
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

        public AuthenticationResult Login(string email, string password)
        {
            //Validate User Does Exist
            if (_userRespository.GetUserByEmail(email) is not User user)
            {
                throw new Exception("User does not exist");
            }

            //Validate Password
            if (user.Password != password)
            {
                throw new Exception("Invalid Password");
            }

            var token = _jwtTokenGenerator.GenerateToken(user);


            return new AuthenticationResult(user, token);

        }
    }
}
