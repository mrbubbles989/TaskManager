using System;
using System.Collections.Generic;
using System.Text;
using log4net;

namespace TaskManager.Common.Logging
{
	public interface ILogManager
	{
		ILog GetLog(Type typeAssociatedWithRequestedLog);
	}
}
