using BuberDinner.Domain.Entites;

namespace BuberDinner.Application.Services.Authentication
{
    public record AuthenticationResult(User User, string Token);
}
