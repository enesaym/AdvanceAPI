using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceApi.LOG.Log
{
	public class Log : ILog
	{
		private readonly Logger _logger;
        public Log()
        {
			_logger = LogManager.GetCurrentClassLogger();
		}
        public void TakeInfoLog(string message)
		{
			_logger.Info(message);
		}
		public void TakeErrorLog(string message)
		{
			_logger.Error(message);
		}
	}
}
