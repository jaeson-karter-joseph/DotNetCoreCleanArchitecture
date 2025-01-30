using BuberDinner.Domain.Entites;

namespace BuberDinner.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
