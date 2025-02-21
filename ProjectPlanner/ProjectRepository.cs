using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPlanner
{
    class ProjectRepository: IProjectRepository
    {
        private ProjectPlannerContext _context;

        public ProjectRepository(ProjectPlannerContext context)
        {
            this._context = context;
        }

        public async Task<Project?> Get(int projectId)
        {
            return await this._context.Projects.FindAsync(projectId);
        }

        public async Task<List<Project>> GetAll()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<bool> Save(Project project)
        {
            _context.Add(project);
            var entries = await _context.SaveChangesAsync();
            return entries > 0;
        }

        public async Task<bool> UpdateZibby()
        {
            var entries = await _context.SaveChangesAsync();
            return entries > 0;
        }
    }
}
