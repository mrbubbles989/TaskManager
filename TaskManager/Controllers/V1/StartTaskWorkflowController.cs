using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TaskManager.Common;
using TaskManager.MaintenanceProcessing;
using TaskManager.Web.Api.Models;
using TaskManager.Web.Common;
using TaskManager.Web.Common.Routing;

namespace TaskManager.Controllers.V1
{
	[ApiVersion1RoutePrefix("")]
	[UnitOfWorkActionFilter]
	public class StartTaskWorkflowController : ApiController
	{
		private readonly IStartTaskWorkflowProcessor _startTaskWorkflowProcessor;

		public StartTaskWorkflowController(IStartTaskWorkflowProcessor startTaskWorkflowProcessor)
		{
			_startTaskWorkflowProcessor = startTaskWorkflowProcessor;
		}

		[HttpPost]
		[Authorize(Roles = Constants.RoleNames.SeniorWorker)]
		[Route("tasks/{taskId:long}/activations", Name = "StartTaskRoute")]
		public Task StartTask(long taskId)
		{
			var task = _startTaskWorkflowProcessor.StartTask(taskId);
			return task;
		}
	}
}