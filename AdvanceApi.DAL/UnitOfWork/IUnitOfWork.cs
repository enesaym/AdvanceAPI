﻿using AdvanceApi.DAL.Repositories.Abstract;
using AdvanceApi.DAL.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.DAL.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		IUnitDAL UnitDAL { get; }
		IAuthDAL AuthDAL { get; }
		ITitleDAL TitleDAL { get; }
        IEmployeeDAL EmployeeDAL { get; }
		IAdvanceDAL AdvanceDAL { get; }
        IProjectDAL ProjectDAL { get; }
        IStatusDAL StatusDAL { get; }
        IRuleDAL RuleDAL { get; }
        IPaymentDAL PaymentDAL { get; }
        IReceiptDAL ReceiptDAL { get; }
        IAdvanceHistoryDAL AdvanceHistoryDAL { get; }
		void Commit();
		void RollBack();
		void TransactionDispose();
		void BeginTransaction();
	}
}
