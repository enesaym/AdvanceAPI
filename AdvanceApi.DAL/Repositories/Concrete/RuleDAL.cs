using AdvanceApi.CORE.Entities;
using AdvanceApi.DAL.Repositories.Abstract;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rule = AdvanceApi.CORE.Entities.Rule;

namespace AdvanceApi.DAL.Repositories.Concrete
{
    public class RuleDAL :IRuleDAL
    {
        private readonly IDbConnection _connection;
        public RuleDAL(IDbConnection connection)
        {
                _connection = connection;
        }
        public async Task<Rule> GetRuleByTitleId(int Id)
        {
            //sp yazilabilir
            try
            {
                string query = "select * from [Rule] where TitleId=@Id";
                var result = await _connection.QueryFirstOrDefaultAsync<Rule>(query, new { Id });
                return result;
            }
            catch (Exception ex)
            {
                return null;

            }

        }
    }
}
