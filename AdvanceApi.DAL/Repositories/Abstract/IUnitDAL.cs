using AdvanceApi.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.DAL.Repositories.Abstract
{
	public interface IUnitDAL
	{
		Task<List<BusinessUnit>> GetAllUnits();
		Task<BusinessUnit> AddBusinessUnit();
		Task<BusinessUnit> GetByUnitId(int id);

    }
}
