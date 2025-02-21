using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPlanner
{
    interface IUserRepository
    {
        Task<bool> Save(User user);

        List<User> GetUsers();

        Task<User?> GetUser(int id);
    }
}
