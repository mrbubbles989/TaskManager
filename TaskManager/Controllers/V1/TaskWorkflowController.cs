using System.Web.Http;
using TaskManager.Common;
using TaskManager.MaintenanceProcessing;
using TaskManager.Web.Api.Models;
using TaskManager.Web.Common;
using TaskManager.Web.Common.Routing;
using TaskManager.Web.Common.Security;

namespace TaskManager.Controllers.V1
{
	[ApiVersion1RoutePrefix("")]
	[UnitOfWorkActionFilter]
	[Authorize(Roles = Constants.RoleNames.SeniorWorker)]
	public class TaskWorkflowController : ApiController
	{
		private readonly IStartTaskWorkflowProcessor _startTaskWorkflowProcessor;
		private readonly ICompleteTaskWorkflowProcessor _completeTaskWorkflowProcessor;
		private readonly IReactivateTaskWorkflowProcessor _reactivateTaskWorkflowProcessor;

		public TaskWorkflowController(IStartTaskWorkflowProcessor startTaskWorkflowProcessor, ICompleteTaskWorkflowProcessor completeTaskWorkflowProcessor, IReactivateTaskWorkflowProcessor reactivateTaskWorkflowprocessor)
		{
			_startTaskWorkflowProcessor = startTaskWorkflowProcessor;
			_completeTaskWorkflowProcessor = completeTaskWorkflowProcessor;
			_reactivateTaskWorkflowProcessor = reactivateTaskWorkflowprocessor;
		}

		[HttpPost]
		[Route("tasks/{taskId:long}/actvations", Name = "StartTaskRoute")]
		public Task StartTask(long taskId)
		{
			var task = _startTaskWorkflowProcessor.StartTask(taskId);
			return task;
		}

		[HttpPost]
		[Route("tasks/{taskId:long}/completions", Name = "CompleteTaskRoute")]
		public Task CompleteTask(long taskId)
		{
			var task = _completeTaskWorkflowProcessor.CompleteTask(taskId);
			return task;
		}

		[HttpPost]
		[UserAudit]
		[Route("tasks/{taskId:long}/reactivations", Name = "ReactivateTaskRoute")]
		public Task ReactivateTask(long taskId)
		{
			var task = _reactivateTaskWorkflowProcessor.ReactivateTask(taskId);
			return task;
		}

	}
}