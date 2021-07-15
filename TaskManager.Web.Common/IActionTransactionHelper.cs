using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http.Filters;

namespace TaskManager.Web.Common
{
	public interface IActionTransactionHelper
	{
		void BeginTransaction();
		void EndTransaction(HttpActionExecutedContext filterContext);
		void CloseSession();
	}
}
