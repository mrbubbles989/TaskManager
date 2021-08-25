using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TaskManager.Common.Logging;
using TaskManager.Web.Api.Models;
using TaskManager.Web.Common;

namespace TaskManager.MaintenanceProcessing
{
	public class ValidateTaskUpdateRequestAttribute : ActionFilterAttribute
	{
		private readonly ILog _log;
		
		public ValidateTaskUpdateRequestAttribute() : this(WebContainerManager.Get<ILogManager>())
		{

		}

		public ValidateTaskUpdateRequestAttribute(ILogManager logManager)
		{
			_log = logManager.GetLog(typeof(ValidateTaskUpdateRequestAttribute));
		}

		public override bool AllowMultiple
		{
			get { return false; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="actionContext"></param>
		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			var taskId = (long)actionContext.ActionArguments[ActionParameterNames.TaskId];
			var taskFragment = (JObject)actionContext.ActionArguments[ActionParameterNames.TaskFragment];
			_log.DebugFormat("{0} = {1}", ActionParameterNames.TaskFragment, taskFragment);

			if (taskFragment == null)
			{
				const string errorMessage = "Malformed or null request.";
				_log.Debug(errorMessage);
				actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
				return;
			}
			try
			{
				var task = taskFragment.ToObject<Task>();
				if (task.TaskId.HasValue && task.TaskId != taskId)
				{
					const string errorMessage = "Task ids do not match.";
					_log.Debug(errorMessage);
					actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
				}
			}
			catch (JsonException ex)
			{
				_log.Debug(ex.Message);
				actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public static class ActionParameterNames
		{
			public const string TaskFragment = "updatedTask";
			public const string TaskId = "id";
		}
	}
}