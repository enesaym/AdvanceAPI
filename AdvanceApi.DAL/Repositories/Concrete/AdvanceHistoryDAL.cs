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
       
        
        public async Task<List<AdvanceHistory>> GetPendingApprovalAdvances(int employeeId)
        {
            string query = @"select ah.Id,ah.TransactorID,ah.StatusID,ah.Date,ah.ApprovedAmount,e.ID,e.Name,e.Surname,e.TitleID,a.Id,a.ProjectID,a.DesiredDate,a.AdvanceDescription,a.AdvanceAmount,a.RequestDate,ee.Id,ee.Name,ee.Surname,bu.Id,bu.BusinessUnitName,t.Id,t.TitleName from AdvanceHistory ah
              join Employee e on e.ID = ah.TransactorID 
             join Employee uppere on uppere.ID = e.UpperEmployeeID 
             join Advance a on a.ID=ah.AdvanceID
            join Employee ee on ee.ID=a.EmployeeID
            join BusinessUnit bu on bu.ID=ee.BusinessUnitID
              join Title t on t.ID=ee.TitleID 
               where uppere.ID= @EmployeeID";


            var parameters = new
            {
                EmployeeID = employeeId
            };
            //ilk employee avans isteyen , ikinci son onaylayan
            var result = await _connection.QueryAsync<AdvanceHistory, Employee,Advance,Employee,BusinessUnit, Title, AdvanceHistory>(
            query,
            (advanceHistory, transactor, advance,employee,businessUnit, title) =>
            {
                advanceHistory.Advance = advance;
                advanceHistory.Transactor = transactor;
                advanceHistory.Advance.Employee = employee;
                advanceHistory.Advance.Employee.Title = title;
                advanceHistory.Advance.Employee.BusinessUnit = businessUnit;
                return advanceHistory;
            },
         param: parameters

        );
            return result.ToList();
        }
        public async Task<List<AdvanceHistory>> GetAdvanceHistoryByAdvanceId(int advanceId)
        {
            string query = @"SELECT ah.Id,ah.AdvanceID,ah.Date,ah.ApprovedAmount,s.Id,s.StatusName,e.Id,e.Name,e.Surname,e.UpperEmployeeID,e.TitleID,p.Id,p.DeterminedPaymentDate FROM AdvanceHistory ah 
              inner join Status s on s.ID=ah.StatusID
            inner join Employee e on e.ID=ah.TransactorID
             left join Payment p on p.DeterminedPaymentDate=ah.AdvanceID
               where ah.AdvanceID= @AdvanceID";
            var parameters = new
            {
                AdvanceID = advanceId
            };
            //EMPLOYEE  : islem yapan 
            var result = await _connection.QueryAsync<AdvanceHistory, Status, Employee, Payment, AdvanceHistory>(
            query,
            (advanceHistory, status,employee, payment) =>
            {
             
                advanceHistory.Status = status;
                advanceHistory.Transactor = employee;
                if (payment != null)
                {
                    advanceHistory.Advance.Payments.Add(payment);
                }
                return advanceHistory;
            },
         param: parameters

        );

            return result.ToList();
        }


    }
}
