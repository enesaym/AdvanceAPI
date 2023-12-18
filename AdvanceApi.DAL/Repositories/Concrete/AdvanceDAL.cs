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
    public class AdvanceDAL : IAdvanceDAL
    {
        private readonly IDbConnection _connection;
        IDbTransaction _transaction;
        public AdvanceDAL(IDbConnection dbConnection, IDbTransaction transaction)
        {
            _connection = dbConnection;
            _transaction = transaction;
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

            int insertedId = await _connection.ExecuteScalarAsync<int>(sqlQuery, parameters, transaction: _transaction);
            return insertedId;
        }
        /// <summary>
        /// employee ıd alır ve kisinin gecmis avans taleplerini detayları ile getirir
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>advance details</returns>
        public async Task<List<Advance>> GetEmployeeAdvances(int employeeId)
        {
            string query = @"SELECT a.Id, a.AdvanceAmount, a.AdvanceDescription, a.ProjectID, a.DesiredDate, a.RequestDate, a.StatusID, a.EmployeeID,
                    p.Id, p.DeterminedPaymentDate,
                    r.Id, r.ReceiptNo, r.Date, r.isRefundReceipt,
                    ah.Id, ah.TransactorID, ah.Date, ah.StatusID, ah.ApprovedAmount,
                    e.Id, e.Name, e.Surname,
                    t.Id, t.TitleName
                    FROM Advance a
                    LEFT JOIN Payment p on p.AdvanceID=a.Id
                    LEFT JOIN Receipt r on r.AdvanceID=a.Id
                    LEFT JOIN AdvanceHistory ah on ah.AdvanceID = a.ID
                    LEFT JOIN Employee e on e.ID = ah.TransactorID
                    LEFT JOIN Title t on t.ID = e.TitleID
                    WHERE a.EmployeeID = @EmployeeID";

            var advances = new Dictionary<int, Advance>();

            var parameters = new
            {
                EmployeeID = employeeId
            };

            var result = await _connection.QueryAsync<Advance, Payment, Receipt, AdvanceHistory, Employee, Title, Advance>(query, (advance, payment, receipt, advancehistory, transactor, title) =>
            {
                if (!advances.TryGetValue(advance.ID, out Advance advanceEntry))
                {
                    advanceEntry = advance;
                    advanceEntry.Project = new Project();
                    advanceEntry.Status = new Status();
                    advanceEntry.Payments = new List<Payment>();
                    advanceEntry.Receipts = new List<Receipt>();
                    advanceEntry.AdvanceHistories = new List<AdvanceHistory>();
                    advances.Add(advance.ID, advanceEntry);
                }

                if (payment != null && !advanceEntry.Payments.Any(x => x.ID == payment.ID))
                    advanceEntry.Payments.Add(payment);

                if (receipt != null && !advanceEntry.Receipts.Any(x => x.ID == receipt.ID))
                    advanceEntry.Receipts.Add(receipt);

                if (advancehistory != null && !advanceEntry.AdvanceHistories.Any(x => x.ID == advancehistory.ID))
                {
                    transactor.Title = title;
                    advancehistory.Transactor = transactor;
                    advanceEntry.AdvanceHistories.Add(advancehistory);
                }

                return advanceEntry;
            }, parameters);

            return advances.Values.ToList();
        }
        
        public async Task<bool> UpdateAdvanceStatus(int advanceId)
        {
            string query = @"Update Advance set StatusID= @NewStatusID 
                    WHERE ID = @AdvanceID";
			var parameters = new
			{
				NewStatusID = 103,
				AdvanceID = advanceId
			};
			var success = false;
			try
            {		
				int rowsAffected =await _connection.ExecuteAsync(query, parameters,transaction: _transaction);
                if(rowsAffected > 0)
                {
                    success=true;
                }
                return success;
			}
            catch (Exception ex)
            {
                //loglanacak
                return success;
            }
               
        }


    }
}
