using AdvanceApi.CORE.Entities;
using AdvanceApi.CORE.Response;
using AdvanceApi.DAL.Repositories.Abstract;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.DAL.Repositories.Concrete
{
	public class UnitDAL : IUnitDAL
	{
		private readonly IDbConnection _connection;
        public UnitDAL(IDbConnection connection)
        {
				_connection= connection;
        }
        public Task<BusinessUnit> AddBusinessUnit()
		{
			throw new NotImplementedException();
		}

		public async Task<List<BusinessUnit>> GetAllUnits()
		{
			try
			{
				string query = "select * from BusinessUnit";
				var sonuc=await _connection.QueryAsync<BusinessUnit>(query);
				return sonuc.ToList();
			}
			catch (Exception ex)
			{
				return null;
			
			}
			
		}

	}
}
