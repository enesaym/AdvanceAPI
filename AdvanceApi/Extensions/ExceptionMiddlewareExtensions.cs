using AdvanceApi.DTO.Error;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace AdvanceApi.Extensions
{
	public static class ExceptionMiddlewareExtensions
	{
		public static void ConfigureExcepitonHandler(this IApplicationBuilder app)
		{
			app.UseExceptionHandler(appError =>
			{
				appError.Run(async context =>
				{
					context.Response.ContentType = "application/json";
					var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

					if (contextFeature is not null)
					{
						context.Response.StatusCode = contextFeature.Error switch
						{
							 SqlException => StatusCodes.Status404NotFound,


							_ => StatusCodes.Status500InternalServerError
						};

						// log alınabilir

						await context.Response.WriteAsync(new ErrorDetails()
						{
							StatusCodes = context.Response.StatusCode,
							Message = contextFeature.Error.Message
						}.ToString());
					}
				});
			});
		}
	}
}
