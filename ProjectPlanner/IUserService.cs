using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPlanner
{
    interface IUserService
    {
         Task<bool> Create(string username);

        List<User> GetUsers();

        Task<User> GetUser(int id);
    }
}
