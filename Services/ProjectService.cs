using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.Models;

namespace EmployeeManagement.Services
{
    public class ProjectService : IProjectService
    {
        private List<Project> _projects = new List<Project>();

        public IEnumerable<Project> GetAll()
        {
            return _projects;
        }

        public Project GetById(int id)
        {
            return _projects.FirstOrDefault(x => x.Id == id);
        }

        public Project Create(Project project)
        {
            project.Id = _projects.Count > 0 ? _projects.Max(x => x.Id) + 1 : 1;
            _projects.Add(project);
            return project;
        }

        public void Update(Project projectParam)
        {
            var project = _projects.FirstOrDefault(x => x.Id == projectParam.Id);
            if (project == null)
                return;

            project.Name = projectParam.Name;
            project.Description = projectParam.Description;
        }

        public void Delete(int id)
        {
            var project = _projects.FirstOrDefault(x => x.Id == id);
            if (project != null)
            {
                _projects.Remove(project);
            }
        }
    }
}
