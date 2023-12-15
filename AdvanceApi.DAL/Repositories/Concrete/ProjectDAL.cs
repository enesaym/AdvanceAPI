using AdvanceApi.CORE.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.DAL.Repositories.Concrete
{
    public class ProjectDAL
    {
        IDbConnection _connection;
        public ProjectDAL(IDbConnection connection)
        {
                _connection = connection;
        }
        public async Task<List<Project>> GetProjectByEmployeeID(int id)
        {
            try
            {
                string query = "select * from EmployeeProject\r\n  inner join Project on EmployeeProject.ProjectID=Project.ID where EmployeeProject.EmployeeID=@ID";
                var result = await _connection.QueryAsync<Project>(query);
                return result.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
