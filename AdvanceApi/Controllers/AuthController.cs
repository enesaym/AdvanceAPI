using AdvanceApi.BLL.Manager;
using AdvanceApi.CORE.Entities;
using AdvanceApi.DTO.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System;
using System.Threading.Tasks;
using AdvanceApi.Extensions;
using Microsoft.Extensions.Configuration;

namespace AdvanceApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		AuthManager _authManager;
		IConfiguration _configuration;
		public AuthController(AuthManager authManager,IConfiguration configuration)
		{
			_authManager = authManager;
			_configuration = configuration;
		}
		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] EmployeeLoginDTO dto)
		{
			var employee = await _authManager.Login(dto);
			if (employee == null)
			{
		
				return null;
			}
			var token = JwtTokenExtension.GenerateJwtToken(_configuration,employee.Data.Email);
			return Ok(token);
		}


		[HttpPost("Register")]
		public async Task<IActionResult> Register([FromBody] EmployeeRegisterDTO dto)
		{
			var user = await _authManager.Register(dto);
			return Ok(user);
		}
	}
}
