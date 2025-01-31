
using BuberDinner.Domain.Entites;

namespace BuberDinner.Application.Common.Interfaces.Persistence
{
    public interface IUserRespository
    {
        User? GetUserByEmail(string email);
        void AddUser(User user);
    }
}
