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
	[ApiVersion1RoutePrefix("tasks")]
	[UnitOfWorkActionFilter]
	[Authorize(Roles = Constants.RoleNames.SeniorWorker)]
	public class TaskUsersController : ApiController
	{
		private readonly ITaskUsersMaintenanceProcessor _taskUsersMaintenanceProcessor;

		public TaskUsersController(ITaskUsersMaintenanceProcessor taskUsersMaintenanceProcessor)
		{
			_taskUsersMaintenanceProcessor = taskUsersMaintenanceProcessor;
		}

		[Route("{taskId:long}/users", Name = "ReplaceTaskUsersRoute")]
		[HttpPut]
		public Task ReplaceTaskUsers(long taskId, [FromBody] IEnumerable<long> userIds)
		{
			var task = _taskUsersMaintenanceProcessor.ReplaceTaskUsers(taskId, userIds);
			return task;
		}

		[Route("{taskId:long}/users", Name = "DeleteTaskUsersRoute")]
		[HttpDelete]
		public Task DeleteTaskUsers(long taskId)
		{
			var task = _taskUsersMaintenanceProcessor.DeleteTaskUsers(taskId);
			return task;
		}

		[Route("{taskId:long}/users/{userId:long}", Name = "AddTaskUserRoute")]
		[HttpPut]
		public Task AddTaskUser(long taskId, long userId)
		{
			var task = _taskUsersMaintenanceProcessor.AddTaskUser(taskId, userId);
			return task;
		}

		[Route("{taskId:long}/users/{userId:long}", Name = "DeleteTaskUserRoute")]
		[HttpDelete]
		public Task DeleteTaskUser(long taskId, long userId)
		{
			var task = _taskUsersMaintenanceProcessor.DeleteTaskUser(taskId, userId);
			return task;
		}
	}
}