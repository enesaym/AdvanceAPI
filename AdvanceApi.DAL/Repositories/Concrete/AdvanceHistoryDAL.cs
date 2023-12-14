using AdvanceApi.CORE.Entities;
using AdvanceApi.DAL.Repositories.Abstract;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AdvanceApi.DAL.Repositories.Concrete
{
	public class AdvanceHistoryDAL :IAdvanceHistoryDAL
	{
        IDbConnection _connection;
		IDbTransaction _transaction;
        public AdvanceHistoryDAL(IDbConnection dbConnection,IDbTransaction transaction)
        {
                _connection = dbConnection;
			_transaction = transaction;
        }
        public async Task<AdvanceHistory> InsertAdvanceHistory(AdvanceHistory advanceHistory)
        {
			try
			{
				string query = @"
            INSERT INTO AdvanceHistory (StatusID, AdvanceID, TransactorID, ApprovedAmount, Date)
            VALUES (@StatusID, @AdvanceID, @TransactorID, @ApprovedAmount, @Date)";

				var parameters = new DynamicParameters();
				parameters.Add("@StatusID", advanceHistory.StatusID);
				parameters.Add("@AdvanceID", advanceHistory.AdvanceID);
				parameters.Add("@TransactorID", advanceHistory.TransactorID);
				parameters.Add("@ApprovedAmount", advanceHistory.ApprovedAmount);
				parameters.Add("@Date", advanceHistory.Date);

				var rowsAffected = await _connection.ExecuteAsync(query, parameters,transaction:_transaction);

				return rowsAffected > 0 ? advanceHistory : null;
			}
			catch (Exception ex)
			{
			
				throw new Exception(ex.Message);
			}
			

		}
    }
}
