using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TaskManager.Web.Common
{
	public class UnitOfWorkActionFilterAttribute : ActionFilterAttribute
	{
		/// <summary>
		/// Provides access to the IActionTransactionHelper dependency. Necessary because constructor injection isn't possible with an attribute.
		/// </summary>
		public virtual IActionTransactionHelper ActionTransactionHelper
		{
			get { return WebContainerManager.Get<IActionTransactionHelper>(); }
		}

		/// <summary>
		/// Prevents the filter from executing multiple times on the same call.
		/// </summary>
		public override bool AllowMultiple
		{
			get { return false; }
		}

		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			ActionTransactionHelper.BeginTransaction();
		}

		public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
		{
			ActionTransactionHelper.EndTransaction(actionExecutedContext);
			ActionTransactionHelper.CloseSession();
		}

	}
}
