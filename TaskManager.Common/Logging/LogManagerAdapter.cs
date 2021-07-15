using System;
using System.Collections.Generic;
using System.Text;
using log4net;

namespace TaskManager.Common.Logging
{
	public class LogManagerAdapter : ILogManager
	{
		public ILog GetLog(Type typeAssociatedWithRequestedLog)
		{
			var log = LogManager.GetLogger(typeAssociatedWithRequestedLog);
			return log;
		}
	}
}
