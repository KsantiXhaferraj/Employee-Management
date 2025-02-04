using System.Collections.Generic;
using EmployeeManagement.Models;

namespace EmployeeManagement.Services
{
    public interface IEmployeeService
    {
        Employee Authenticate(string email, string password);
        IEnumerable<Employee> GetAll();
        Employee GetById(int id);
        Employee Create(Employee employee);
        void Update(Employee employee);
        void Delete(int id);
    }
}
