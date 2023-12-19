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
    public class PaymentDAL :IPaymentDAL
    {
        private readonly IDbConnection _connection;
        IDbTransaction _transaction;
        public PaymentDAL(IDbConnection connection,IDbTransaction transaction)
        {
                _connection = connection;
                _transaction = transaction;
        }

        public async Task<int> PaymentInsert(Payment payment)
        {
            try
            {
                string sqlQuery = @"
        INSERT INTO Payment (DeterminedPaymentDate, FinanceManagerID, AdvanceId)
        VALUES (@DeterminedPaymentDate, @FinanceManagerID, @AdvanceId) SELECT CAST(SCOPE_IDENTITY() as int);";

                var parameters = new DynamicParameters();
                parameters.Add("@DeterminedPaymentDate", payment.DeterminedPaymentDate);
                parameters.Add("@FinanceManagerID", payment.FinanceManagerID);
                parameters.Add("@AdvanceId", payment.AdvanceId);

                var insertedId = await _connection.ExecuteScalarAsync<int>(sqlQuery, parameters, transaction: _transaction);
                return insertedId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
           
        }
    }
}
