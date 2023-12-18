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
    public class EmployeeDAL :IEmployeeDAL
    {
        IDbConnection _connection;
        public EmployeeDAL(IDbConnection dbConnection)
        {
                _connection = dbConnection;
        }
        public async Task<List<Employee>> GetEmployeeBase()
        {
            try
            {
                string query = "select ID,Name,Surname,PhoneNumber,Email from Employee";
                var result = await _connection.QueryAsync<Employee>(query);
                return result.ToList();
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public async Task<Employee> GetByEmployeeId(int Id)
        {
            try
            {
                string query = "select * from Employee where Id=@Id";
                var result = await _connection.QueryFirstOrDefaultAsync<Employee>(query, new { Id });
                return result;
            }
            catch (Exception ex)
            {
                return null;

            }

        }



    }
}
