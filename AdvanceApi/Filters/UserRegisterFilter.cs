using AdvanceApi.DTO.Employee;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using AdvanceApi.BLL.Manager;

namespace AdvanceApi.Filters
{
	public class UserRegisterFilter : Attribute, IAsyncActionFilter
	{
		private readonly AuthManager _authManager;

		public UserRegisterFilter(AuthManager authManager)
		{
			_authManager = authManager;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			if (context.ActionArguments.TryGetValue("dto", out object dtoObject) && dtoObject is EmployeeRegisterDTO dto)
			{
				var existingUser = await _authManager.Register(dto);

				if (existingUser != null)
				{
					// Kullanıcı varsa BadRequest döndür
					context.Result = new BadRequestObjectResult("Kullanıcı zaten var.");
					return;
				}
			}

			await next();
		}
	}
	}

