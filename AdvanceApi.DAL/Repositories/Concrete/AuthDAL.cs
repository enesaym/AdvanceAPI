using AdvanceApi.CORE.Entities;
using AdvanceApi.DAL.Repositories.Abstract;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AdvanceApi.DAL.Repositories.Concrete
{
	public class AuthDAL :IAuthDAL
	{
        private readonly IDbConnection _connection;
        public AuthDAL(IDbConnection connection)
        {
                _connection = connection;
        }
		public async Task<Employee> Login(Employee employee, string password)
		{
			var query ="select e.*, t.ID , t.TitleName from Employee e LEFT JOIN Title t ON e.TitleID = t.ID where e.Email = @Email";
			var parameters = new DynamicParameters();
			parameters.Add("@Email", employee.Email, DbType.String);

			var data = await _connection.QueryAsync<Employee, Title, Employee>(
				query,
				(emp, title) =>
				{
					emp.Title = title;
					return emp;
				},
			
				parameters,
				splitOn: "ID");

			var user = data.FirstOrDefault();
			if (user == null)
			{
				return null;
			}

			if (!CheckPassword(password, user.PasswordSalt, user.PasswordHash))
			{
				return null;
			}

			return user;
		}
		private bool CheckPassword(string password, byte[] passSalt, byte[] passwordHash)
		{
			using (var hmac = new HMACSHA512(passSalt))
			{
				var _passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
				for (int i = 0; i < _passwordHash.Length; i++)
				{
					if (passwordHash[i] != _passwordHash[i])
					{
						return false;
					}
				}
				return true;
			}
		}

		public async Task<Employee> Register(Employee employee,string password)
		{ 

			byte[] passHash, passSalt;
			CreatePass(password, out passHash, out passSalt);
			employee.PasswordHash = passHash;
			employee.PasswordSalt = passSalt;

			var existingUser = await GetUserByEmail(employee.Email);
			if (existingUser != null)
			{
				return null;
			}


			var sqlquery = "Insert into Employee (Name,Surname,PhoneNumber,Email,PasswordHash,PasswordSalt,BusinessUnitID,TitleID,UpperEmployeeID) Values (@Name,@Surname,@PhoneNumber,@Email,@PasswordHash,@PasswordSalt,@BusinessUnitID,@TitleID,@UpperEmployeeID)";

			var parameters = new DynamicParameters();

			parameters.Add("@Name", employee.Name, DbType.String);
			parameters.Add("@Surname", employee.Surname, DbType.String);
			parameters.Add("@PhoneNumber", employee.PhoneNumber, DbType.String);
			parameters.Add("@Email", employee.Email, DbType.String);
			parameters.Add("@PasswordHash", passHash, DbType.Binary);
			parameters.Add("@PasswordSalt", passSalt, DbType.Binary);
			parameters.Add("@BusinessUnitID", employee.BusinessUnitID, DbType.Int32);
			parameters.Add("@TitleID", employee.TitleID, DbType.Int32);
			parameters.Add("@UpperEmployeeID", employee.UpperEmployeeID, DbType.Int32);

			var rowsAffected =await _connection.ExecuteAsync(sqlquery, parameters);

			return rowsAffected >0 ? employee : null;
		}

		void CreatePass(string password, out byte[] pasHash, out byte[] passSalt)
		{
			{
				using (var hmac = new HMACSHA512())
				{
					pasHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
					passSalt = hmac.Key;
				}
			}
		}
		public async Task<Employee> GetUserByEmail(string email)
		{
			var sqlQuery = "SELECT * FROM Employee WHERE Email = @Email";
			var parameters = new DynamicParameters();
			parameters.Add("@Email", email, DbType.String);

			var user = await _connection.QueryFirstOrDefaultAsync<Employee>(sqlQuery, parameters);
			return user;
		}

	}
}
