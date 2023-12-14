using AdvanceApi.DAL.Repositories.Abstract;
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
        void Commit();
		void BeginTransaction();
	}
}
