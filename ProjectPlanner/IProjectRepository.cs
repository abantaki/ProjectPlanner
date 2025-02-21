using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPlanner
{
    interface IProjectRepository
    {

        Task<Project?> Get(int projectId);
        Task<List<Project>> GetAll();

        Task<bool> Save(Project project);
        Task<bool> UpdateZibby();

    }
}
