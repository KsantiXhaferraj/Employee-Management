using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.Models;

namespace EmployeeManagement.Services
{
    public class TaskService : ITaskService
    {
        private List<Task> _tasks = new List<Task>();

        public IEnumerable<Task> GetAll()
        {
            return _tasks;
        }

        public Task GetById(int id)
        {
            return _tasks.FirstOrDefault(x => x.Id == id);
        }

        public Task Create(Task task)
        {
            task.Id = _tasks.Count > 0 ? _tasks.Max(x => x.Id) + 1 : 1;
            _tasks.Add(task);
            return task;
        }

        public void Update(Task taskParam)
        {
            var task = _tasks.FirstOrDefault(x => x.Id == taskParam.Id);
            if (task == null)
                return;

            task.Title = taskParam.Title;
            task.Description = taskParam.Description;
            task.ProjectId = taskParam.ProjectId;
            task.AssignedToId = taskParam.AssignedToId;
            task.DueDate = taskParam.DueDate;
            task.IsCompleted = taskParam.IsCompleted;
        }

        public void Delete(int id)
        {
            var task = _tasks.FirstOrDefault(x => x.Id == id);
            if (task != null)
            {
                _tasks.Remove(task);
            }
        }
    }
}
