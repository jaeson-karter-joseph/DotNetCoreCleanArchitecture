using BuberDinner.Domain.Entites;

namespace BuberDinner.Application.Authentication.Common
{
    public record AuthenticationResult(User User, string Token);
}
