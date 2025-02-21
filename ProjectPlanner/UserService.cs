using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPlanner
{
    class UserService: IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository repository)
        {
            _userRepository = repository;
        }
        public async Task<bool> Create(string username)
        {
            Console.WriteLine($"Attemtping to create user {username}");
            return await _userRepository.Save(new User { Username = username });
        }

        public List<User> GetUsers()
        {
            var users = _userRepository.GetUsers();
            return users;

        }

        public async Task<User?> GetUser(int id)
        {
            return await _userRepository.GetUser(id);
        }
    }
}
