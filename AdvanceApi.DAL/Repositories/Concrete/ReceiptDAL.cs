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
    public class ReceiptDAL :IReceiptDAL
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;
        public ReceiptDAL(IDbConnection connection,IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public async Task<int> ReceiptInsert(Receipt receipt)
        {
            try
            {
                string sqlQuery = @"
            INSERT INTO Receipt (ReceiptNo, isRefundReceipt, AdvanceID, Date, AccountantID)
            VALUES (@ReceiptNo,  @isRefundReceipt, @AdvanceID, @Date, @AccountantID);
            SELECT CAST(SCOPE_IDENTITY() as int);";

                var parameters = new DynamicParameters();
                parameters.Add("@ReceiptNo", receipt.ReceiptNo);
                parameters.Add("@isRefundReceipt", receipt.isRefundReceipt);
                parameters.Add("@AdvanceID", receipt.AdvanceID);
                parameters.Add("@Date", receipt.Date);
                parameters.Add("@AccountantID", receipt.AccountantID);

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
