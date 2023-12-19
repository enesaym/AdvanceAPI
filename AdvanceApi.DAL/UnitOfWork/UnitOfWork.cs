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
        private IEmployeeDAL _employeeDAL;
		private IAdvanceDAL _advanceDAL;
		private IAdvanceHistoryDAL _advanceHistoryDAL;
        private IProjectDAL _projectDAL;
        private IStatusDAL _statusDAL;
        private IRuleDAL _ruleDAL;
        private IPaymentDAL _paymentDAL;
        private IReceiptDAL _receiptDAL;


        public UnitOfWork(IDbConnection conn)
		{
			_connection = conn;
			_connection.Open();
		}

        //IQuestionRepository çünkü QuestionRepository yerine başka bir class daha newleyip gönderebiliriz. örn: QuestionRepository2
        #region new dals
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
        public IEmployeeDAL EmployeeDAL
        {
            get { return _employeeDAL ?? (_employeeDAL = new EmployeeDAL(_connection)); }
        }
		public IAdvanceDAL AdvanceDAL
		{
			get { return _advanceDAL ?? (_advanceDAL = new AdvanceDAL(_connection,_transaction)); }
		}
		public IAdvanceHistoryDAL AdvanceHistoryDAL
		{
			get { return _advanceHistoryDAL ?? (_advanceHistoryDAL = new AdvanceHistoryDAL(_connection,_transaction)); }
		}
        public IProjectDAL ProjectDAL
        {
            get { return _projectDAL ?? (_projectDAL = new ProjectDAL(_connection)); }
        }
        public IStatusDAL StatusDAL
        {
            get { return _statusDAL ?? (_statusDAL = new StatusDAL(_connection)); }
        }
        public IRuleDAL RuleDAL
        {
            get { return _ruleDAL ?? (_ruleDAL = new RuleDAL(_connection)); }
        }
        public IPaymentDAL PaymentDAL
        {
            get { return _paymentDAL ?? (_paymentDAL = new PaymentDAL(_connection,_transaction)); }
        }
        public IReceiptDAL ReceiptDAL
        {
            get { return _receiptDAL ?? (_receiptDAL = new ReceiptDAL(_connection, _transaction)); }
        }
        #endregion 

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
			_transaction.Commit();	
		}
		public void RollBack()
		{
			_transaction.Rollback();
		}
		public void TransactionDispose() 
		{
			_transaction.Dispose();
		}

		//public void Commit()
		//{
		//	try
		//	{
		//		_transaction.Commit();
		//	}
		//	catch
		//	{
		//		_transaction.Rollback();
		//	}
		//	finally
		//	{
		//		_transaction.Dispose();

		//	}
		//}	

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
