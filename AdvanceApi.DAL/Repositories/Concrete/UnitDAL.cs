using AdvanceApi.CORE.Entities;
using AdvanceApi.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.DAL.Repositories.Concrete
{
	public class UnitDAL : IUnitDAL
	{
		IDbConnection _conn;
        public UnitDAL(IDbConnection conn)
        {
				_conn=conn;	
        }
        public Task<BusinessUnit> AddBusinessUnit()
		{

			throw new NotImplementedException();
		}

		public Task<BusinessUnit> GetAllUnits()
		{

			throw new NotImplementedException();
		}
	}
}
