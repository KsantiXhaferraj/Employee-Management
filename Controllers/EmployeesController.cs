using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models;
using EmployeeManagement.Services;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IConfiguration _configuration;

        public EmployeesController(IEmployeeService employeeService, IConfiguration configuration)
        {
            _employeeService = employeeService;
            _configuration = configuration;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] AuthenticateRequest model)
        {
            var employee = _employeeService.Authenticate(model.Email, model.Password);

            if (employee == null)
                return BadRequest(new { message = "Invalid Credentials" });

            var token = GenerateJwtToken(employee);
            return Ok(new AuthenticateResponse
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Role = employee.Role,
                Token = token
            });
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult GetAll()
        {
            var employees = _employeeService.GetAll();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
                return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create([FromBody] Employee model)
        {
            var employee = _employeeService.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, [FromBody] Employee model)
        {
            model.Id = id;
            _employeeService.Update(model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int id)
        {
            _employeeService.Delete(id);
            return NoContent();
        }

        private string GenerateJwtToken(Employee employee)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", employee.Id.ToString()),
                    new Claim(ClaimTypes.Role, employee.Role),
                    new Claim(ClaimTypes.Email, employee.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
