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
	public class AdvanceDAL :IAdvanceDAL
	{
        IDbConnection _connection;
		IDbTransaction _transaction;
        public AdvanceDAL(IDbConnection dbConnection,IDbTransaction transaction)
        {
                _connection = dbConnection;
			_transaction= transaction;
        }
        public async Task<int> AdvanceInsert(Advance advance)
        {
			//eklenen kaydın id sini döndürecek
			string sqlQuery = @"
            INSERT INTO Advance (AdvanceAmount, AdvanceDescription, ProjectID, DesiredDate, StatusID, RequestDate, EmployeeID)
            VALUES (@AdvanceAmount, @AdvanceDescription, @ProjectID, @DesiredDate, @StatusID,@RequestDate, @EmployeeID);
            SELECT CAST(SCOPE_IDENTITY() as int);";

			var parameters = new DynamicParameters();
			parameters.Add("@AdvanceAmount", advance.AdvanceAmount);
			parameters.Add("@AdvanceDescription", advance.AdvanceDescription);
			parameters.Add("@ProjectID", advance.ProjectID);
			parameters.Add("@DesiredDate", advance.DesiredDate);
			parameters.Add("@StatusID", advance.StatusID);
			parameters.Add("@RequestDate", advance.RequestDate);
			parameters.Add("@EmployeeID", advance.EmployeeID);


			int insertedId = await _connection.ExecuteScalarAsync<int>(sqlQuery, parameters,transaction:_transaction);
			return insertedId;
        }

    }
}
