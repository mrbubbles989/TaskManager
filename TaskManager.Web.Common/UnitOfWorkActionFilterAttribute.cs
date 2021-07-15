﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TaskManager.Web.Common
{
	public class UnitOfWorkActionFilterAttribute : ActionFilterAttribute
	{
		public virtual IActionTransactionHelper ActionTransactionHelper
		{
			get { return WebContainerManager.Get<IActionTransactionHelper>(); }
		}

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
			ActionTransactionHelper.CloseSession;
		}

	}
}
