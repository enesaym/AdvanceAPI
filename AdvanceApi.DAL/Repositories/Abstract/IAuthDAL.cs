using AdvanceApi.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.DAL.Repositories.Abstract
{
	public interface IAuthDAL
	{
		Task<Employee> Register(Employee employee, string password);
		Task<Employee> Login(Employee employee, string password);
	}
}
