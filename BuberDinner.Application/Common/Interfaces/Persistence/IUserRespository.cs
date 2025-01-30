
using BuberDinner.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Common.Interfaces.Persistence
{
    public interface IUserRespository
    {
        User? GetUserByEmail(string email);
        void AddUser(User user);
    }
}
