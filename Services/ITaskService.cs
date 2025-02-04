using System.Collections.Generic;
using EmployeeManagement.Models;

namespace EmployeeManagement.Services
{
    public interface ITaskService
    {
        IEnumerable<Task> GetAll();
        Task GetById(int id);
        Task Create(Task task);
        void Update(Task task);
        void Delete(int id);
    }
}
