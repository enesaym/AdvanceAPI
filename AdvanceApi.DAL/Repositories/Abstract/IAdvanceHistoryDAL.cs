using AdvanceApi.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.DAL.Repositories.Abstract
{
	public interface IAdvanceHistoryDAL
	{
		Task<AdvanceHistory> InsertAdvanceHistory(AdvanceHistory advanceHistory);
		Task<List<AdvanceHistory>> GetPendingApprovalAdvances(int employeeId);
		Task<List<AdvanceHistory>> GetAdvanceHistoryByAdvanceId(int advanceId);
		Task<bool> UpdateAdvanceHistoriesByAdvanceID(int advanceId);

    }
}
