using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entites;

namespace BuberDinner.Infrastructure.Persistence
{
    public class UserRepository : IUserRespository
    {
        private static readonly List<User> _users = [];
        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public User? GetUserByEmail(string email)
        {
            return _users.SingleOrDefault(x => x.Email == email);
        }
    }
}
