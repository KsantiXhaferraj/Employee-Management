using System.Collections.Generic;
using EmployeeManagement.Models;

namespace EmployeeManagement.Services
{
    public interface IProjectService
    {
        IEnumerable<Project> GetAll();
        Project GetById(int id);
        Project Create(Project project);
        void Update(Project project);
        void Delete(int id);
    }
}
