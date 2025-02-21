using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPlanner
{
    interface IProjectService
    {
        Task<Project> Get(int projectId);
        Task<List<Project>> GetAll();
        Task<bool> Add(string name, string customer, User user);
        Task<bool> UpdateStatus(int projectId, string newStatus);
    }
}
