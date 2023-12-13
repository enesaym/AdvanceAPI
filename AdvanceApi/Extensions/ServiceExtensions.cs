using AdvanceApi.BLL.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace AdvanceApi.Extensions
{
	public static class ServiceExtensions
	{
		public static void AddServiceExtension(this IServiceCollection services)
		{	
			services.AddScoped<MyMapper>();
		}
	}
}
