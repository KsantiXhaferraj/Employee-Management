using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var projects = _projectService.GetAll();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var project = _projectService.GetById(id);
            if (project == null)
                return NotFound();
            return Ok(project);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Project model)
        {
            var project = _projectService.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Project model)
        {
            model.Id = id;
            _projectService.Update(model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _projectService.Delete(id);
            return NoContent();
        }
    }
}
