using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
