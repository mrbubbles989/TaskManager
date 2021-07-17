using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManager.Web.Api.Models;
using TaskManager.Web.Common.Routing;
using TaskManager.Web.Common;

namespace TaskManager.Web.Api.Controllers.V1
{
	[ApiVersion1RoutePrefix("tasks")]
	[UnitOfWorkActionFilter]
	public class TasksController : ApiController
    {
		[Route("", Name = "AddTaskRoute")]
		[HttpPost]
		public Task AddTask(HttpRequestMessage requestMessage, Task newTask)
		{
			return new Task
			{
				Subject = "In v1, newTask.Subject = " + newTask.Subject
			};
		}
    }
}
