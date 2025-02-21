using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPlanner
{
    class ProjectService: IProjectService
    {
        private IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            this._projectRepository = projectRepository;
        }

        public async Task<bool> Add(string name, string customer, User user)
        {
            var project = new Project { Name = name, Customer = customer, ResponsibleUser = user, ResponsibleUserId = user.Id, StartDate = new DateTime() };

            var success = await _projectRepository.Save(project);
            return success;
        }

        public async Task<List<Project>> GetAll()
        {
            var projects = await _projectRepository.GetAll();
            return projects;

        }

        public async Task<Project> Get(int projectId)
        {
            var project = await _projectRepository.Get(projectId);

            if (project == null)
            {
                throw new Exception("Unable to get Project");
            }

            return project;
        }

        public async Task<bool> UpdateStatus(int projectId, string newStatus)
        {
            var project = await Get(projectId);
            project.Status = newStatus;
            return await _projectRepository.UpdateZibby();

        }
    }
}
