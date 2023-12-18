using AdvanceApi.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.DAL.Repositories.Abstract
{
	public interface IAdvanceDAL
	{
		Task<int> AdvanceInsert(Advance advance);
		Task<List<Advance>> GetEmployeeAdvances(int employeeId);
		Task<bool> UpdateAdvanceStatus(int advanceId);

	}
}
