using AdvanceApi.CORE.Entities;
using AdvanceApi.DAL.Repositories.Abstract;
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
    public class ProjectDAL :IProjectDAL
    {
        private readonly IDbConnection _connection;
        public ProjectDAL(IDbConnection connection)
        {
                _connection = connection;
        }
        public async Task<List<Project>> GetProjectsByEmployeeID(int id)
        {
            try
            {
                string query = "select * from EmployeeProject inner join Project on EmployeeProject.ProjectID=Project.ID where EmployeeProject.EmployeeID=@EmployeeID";
                var result = await _connection.QueryAsync<Project>(query, new { EmployeeID = id });
                return result.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<Project> GetProjectById(int Id)
        {
            try
            {
                var query = "SELECT * FROM Project WHERE Id = @Id";
                var result = await _connection.QueryAsync<Project>(query, new { Id });

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
           
        }



    }
}
