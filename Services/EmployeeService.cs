using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.Models;

namespace EmployeeManagement.Services
{
    public class EmployeeService : IEmployeeService
    {
        private List<Employee> _employees = new List<Employee>();

        public Employee Authenticate(string email, string password)
        {
            var employee = _employees.SingleOrDefault(x => x.Email == email && x.Password == password);

            // return null if user not found
            if (employee == null)
                return null;

            return employee;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _employees;
        }

        public Employee GetById(int id)
        {
            return _employees.FirstOrDefault(x => x.Id == id);
        }

        public Employee Create(Employee employee)
        {
            employee.Id = _employees.Count > 0 ? _employees.Max(x => x.Id) + 1 : 1;
            _employees.Add(employee);
            return employee;
        }

        public void Update(Employee employeeParam)
        {
            var employee = _employees.FirstOrDefault(x => x.Id == employeeParam.Id);
            if (employee == null)
                return;

            employee.FirstName = employeeParam.FirstName;
            employee.LastName = employeeParam.LastName;
            employee.Email = employeeParam.Email;
            employee.Password = employeeParam.Password;
            employee.ProfilePicture = employeeParam.ProfilePicture;
            employee.DateOfBirth = employeeParam.DateOfBirth;
            employee.Role = employeeParam.Role;
        }

        public void Delete(int id)
        {
            var employee = _employees.FirstOrDefault(x => x.Id == id);
            if (employee != null)
            {
                _employees.Remove(employee);
            }
        }
    }
}
