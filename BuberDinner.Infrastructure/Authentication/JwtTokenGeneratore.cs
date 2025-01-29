using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BuberDinner.Infrastructure.Authentication
{
    public class JwtTokenGeneratore(IDateTimeProvider _dateTimeProvider) : IJwtTokenGenerator
    {
        public string GenerateToken(Guid userId, string firstName, string lastName)
        {
            var siginingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this-is-a-very-strong-secret-key!123456")), SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, firstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var securityToken = new JwtSecurityToken(
                issuer: "BubberDinner",
                expires: _dateTimeProvider.UtcNow.AddMinutes(60),
                claims: claims,
                signingCredentials: siginingCredentials);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
