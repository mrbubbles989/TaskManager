using System.Net.Http;
using System.Web.Http;
using TaskManager.Web.Api.Models;
using TaskManager.Web.Common.Routing;
using TaskManager.Web.Common;
using TaskManager.MaintenanceProcessing;
using TaskManager.InquiryProcessing;
using TaskManager.Common;
using TaskManager.Web.Common.Validation;

namespace TaskManager.Web.Api.Controllers.V1
{
	[ApiVersion1RoutePrefix("tasks")]
	[UnitOfWorkActionFilter]
	[Authorize(Roles = Constants.RoleNames.JuniorWorker)]
	public class TasksController : ApiController
    {
		private readonly IAddTaskMaintenanceProcessor _addTaskMaintenanceProcessor;
		private readonly ITaskByIdInquiryProcessor _taskByIdInquiryProcessor;
		private readonly IUpdateTaskMaintenanceProcessor _updateTaskMaintenanceProcessor;
		private readonly IPagedDataRequestFactory _pagedDataRequestFactory;
		private readonly IAllTasksInquiryProcessor _allTasksInquiryProcessor;

		public TasksController(IAddTaskMaintenanceProcessor addTaskMaintenanceProcessor, ITaskByIdInquiryProcessor taskByIdInquiryProcessor, IUpdateTaskMaintenanceProcessor updateTaskMaintenanceProcessor, IPagedDataRequestFactory pagedDataRequestFactory, IAllTasksInquiryProcessor allTasksinquiryProcessor)
		{
			_taskByIdInquiryProcessor = taskByIdInquiryProcessor;
			_addTaskMaintenanceProcessor = addTaskMaintenanceProcessor;
			_updateTaskMaintenanceProcessor = updateTaskMaintenanceProcessor;
			_pagedDataRequestFactory = pagedDataRequestFactory;
			_allTasksInquiryProcessor = allTasksinquiryProcessor;
		}


		[Route("", Name = "AddTaskRoute")]
		[HttpPost]
		[ValidateModel]
		[Authorize(Roles = Constants.RoleNames.Manager)]
		public IHttpActionResult AddTask(HttpRequestMessage requestMessage, NewTask newTask)
		{
			var task = _addTaskMaintenanceProcessor.AddTask(newTask);
			var result = new TaskCreatedActionResult(requestMessage, task);

			return result;
		}

		/// <summary>
		/// Retrieves task
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[Route("{id:long}", Name = "GetTaskRoute")]
		public Task GetTask(long id)
		{
			var task = _taskByIdInquiryProcessor.GetTask(id);
			return task;
		}

		[Route("{id:long}", Name = "UpdateTaskRoute")]
		[HttpPut]
		[HttpPatch]
		[ValidateTaskUpdateRequest]
		[Authorize(Roles = Constants.RoleNames.SeniorWorker)]
		public Task UpdateTask(long id, [FromBody] object updatedTask)
		{
			var task = _updateTaskMaintenanceProcessor.UpdateTask(id, updatedTask);
			return task;
		}

		[Route("", Name = "GetTasksRoute")]
		public PagedDataInquiryResponse<Task> GetTasks(HttpRequestMessage requestMessage)
		{
			var request = _pagedDataRequestFactory.Create(requestMessage.RequestUri);

			var tasks = _allTasksInquiryProcessor.GetTasks(request);
			return tasks;
		}
    }
}
