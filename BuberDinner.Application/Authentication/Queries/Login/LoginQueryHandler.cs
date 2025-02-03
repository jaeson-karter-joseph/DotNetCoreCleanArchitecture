using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entites;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler(IUserRespository _userRespository, IJwtTokenGenerator _jwtTokenGenerator) : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            if (_userRespository.GetUserByEmail(query.Email) is not User user)
            {
                return Domain.Errors.Errors.Authentication.InvalidEmail;
            }

            //Validate Password
            if (user.Password != query.Password)
            {
                return new[] { Domain.Errors.Errors.Authentication.InvalidPassword, Domain.Errors.Errors.Authentication.InvalidEmail };
            }

            var token = _jwtTokenGenerator.GenerateToken(user);


            return new AuthenticationResult(user, token);
        }
    }
}
