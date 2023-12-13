using AdvanceApi.BLL.Manager;
using AdvanceApi.BLL.Mapper;
using AdvanceApi.DAL.Repositories.Abstract;
using AdvanceApi.DAL.Repositories.Concrete;
using AdvanceApi.DAL.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace AdvanceApi.Extensions
{
	public static class ServiceExtensions
	{
		public static void AddServiceExtension(this IServiceCollection services)
		{	
			services.AddScoped<MyMapper>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<BusinessUnitManager>();
			services.AddScoped<AuthManager>();
			services.AddScoped<TitleManager>();
			services.AddScoped<EmployeeManager>();

		}
	}
}
