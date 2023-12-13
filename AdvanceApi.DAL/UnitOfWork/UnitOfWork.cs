using AdvanceApi.DAL.Repositories.Abstract;
using AdvanceApi.DAL.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.DAL.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private IDbConnection _connection;
		private IDbTransaction _transaction;

		private bool _dispose;

		private IUnitDAL _unitDAL;
		private IAuthDAL _authDAL;
		private ITitleDAL _titleDAL;

		public UnitOfWork(IDbConnection conn)
		{
			_connection = conn;
			_connection.Open();
		}

		//IQuestionRepository çünkü QuestionRepository yerine başka bir class daha newleyip gönderebiliriz. örn: QuestionRepository2
		public IUnitDAL UnitDAL
		{
			get { return _unitDAL ?? (_unitDAL = new UnitDAL(_connection)); }
		}

		public IAuthDAL AuthDAL
		{
			get { return _authDAL ?? (_authDAL = new AuthDAL(_connection)); }
		}
		public ITitleDAL TitleDAL
		{
			get { return _titleDAL ?? (_titleDAL = new TitleDAL(_connection)); }
		}


		public void BeginTransaction()
		{
			try
			{
				_transaction = _connection.BeginTransaction();
			}
			catch (Exception)
			{

				throw;
			}
		}

		public void Commit()
		{
			try
			{
				_transaction.Commit();
			}
			catch
			{
				_transaction.Rollback();
			}
			finally
			{
				_transaction.Dispose();
			
			}
		}	

		public void Dispose()
		{
			dispose(true);
			GC.SuppressFinalize(this);
		}

		private void dispose(bool disposing)
		{
			if (!_dispose)
			{
				if (disposing)
				{
					if (_transaction != null)
					{
						_transaction.Dispose();
						_transaction = null;
					}
					if (_connection != null)
					{
						_connection.Dispose();
						_connection = null;
					}
				}
				_dispose = true;
			}
		}
	
	}
}
