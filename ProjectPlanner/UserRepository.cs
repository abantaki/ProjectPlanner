using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPlanner
{
    class UserRepository: IUserRepository
    {
        private ProjectPlannerContext _context;

        public UserRepository(ProjectPlannerContext context)
        {
            _context = context;
        }

        public async Task<bool>Save(User user)
        {
            try
            {
                _context.Add(user);
                var entries = await _context.SaveChangesAsync();
                return entries > 0;
            } catch(DbUpdateException e)
            {
                return false;
            }

        }

        public List<User> GetUsers()
        {
            var users = _context.Users.ToList();
            return users;
        }

        public async Task<User?> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
