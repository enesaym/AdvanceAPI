using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.LOG.Log
{
	public interface ILog
	{
		void TakeInfoLog(string message);
		void TakeErrorLog(string message);
	}
}
