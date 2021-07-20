using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManager.Web.Api.Models;
using TaskManager.Web.Common.Routing;
using TaskManager.Web.Common;
using TaskManager.Web.Api.MaintenanceProcessing;

namespace TaskManager.Web.Api.Controllers.V1
{
	[ApiVersion1RoutePrefix("tasks")]
	[UnitOfWorkActionFilter]
	public class TasksController : ApiController
    {
		private readonly IAddTaskMaintenanceProcessor _addTaskMaintenanceProcessor;

		public TasksController(IAddTaskMaintenanceProcessor _addTaskMaintenanceProcessor)
		{
			_addTaskMaintenanceProcessor = _addTaskMaintenanceProcessor;
		}


		[Route("", Name = "AddTaskRoute")]
		[HttpPost]
		public Task AddTask(HttpRequestMessage requestMessage, NewTask newTask)
		{
			var task = _addTaskMaintenanceProcessor.AddTask(newTask);

			return task;
		}
    }
}
