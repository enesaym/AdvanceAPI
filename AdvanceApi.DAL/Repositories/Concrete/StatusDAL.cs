using AdvanceApi.CORE.Entities;
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
    public class StatusDAL :IStatusDAL
    {
        IDbConnection _connection;
        public StatusDAL(IDbConnection dbConnection)
        {
                _connection = dbConnection;
        }

        public async Task<Status> GetStatusById(int Id)
        {
            try
            {
                var query = "SELECT * FROM Status WHERE Id = @Id";
                var result = await _connection.QueryAsync<Status>(query, new { Id });

                return result.FirstOrDefault();
            }
            catch (Exception)
            {

                return null;
            }
          
        }

    }
}
