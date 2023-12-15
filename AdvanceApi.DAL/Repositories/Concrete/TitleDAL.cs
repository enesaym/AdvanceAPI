using AdvanceApi.CORE.Entities;
using AdvanceApi.CORE.Response;
using AdvanceApi.DAL.Repositories.Abstract;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.DAL.Repositories.Concrete
{
	public class TitleDAL:ITitleDAL
	{
        IDbConnection _connection;
        public TitleDAL(IDbConnection connection)
        {
            _connection = connection;
        }
		public async Task<List<Title>> GetAllTitles()
		{
			try
			{
				string query = "select * from Title";
				var result = await _connection.QueryAsync<Title>(query);
				return result.ToList();
			}
			catch (Exception ex)
			{
				return null;

			}

		}
	}
}
