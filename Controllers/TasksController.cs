using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var tasks = _taskService.GetAll();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var task = _taskService.GetById(id);
            if (task == null)
                return NotFound();
            return Ok(task);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Employee")]
        public IActionResult Create([FromBody] Task model)
        {
            var task = _taskService.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator, Employee")]
        public IActionResult Update(int id, [FromBody] Task model)
        {
            model.Id = id;
            _taskService.Update(model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int id)
        {
            _taskService.Delete(id);
            return NoContent();
        }
    }
}
