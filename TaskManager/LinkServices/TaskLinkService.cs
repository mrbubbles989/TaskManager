using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using TaskManager.Common;
using TaskManager.Web.Api.Models;

namespace TaskManager.LinkServices
{
	public class TaskLinkService : ITaskLinkService
	{
		private readonly ICommonLinkService _commonLinkService;
		private readonly IUserLinkService _userLinkService;


		public TaskLinkService(ICommonLinkService commonLinkService, IUserLinkService userLinkService)
		{
			_commonLinkService = commonLinkService;
			_userLinkService = userLinkService;
		}

		/// <summary>
		/// Iterates the assignees list in order to add the appropriate link to each user
		/// </summary>
		/// <param name="task"></param>
		public void AddLinksToChildObjects(Task task)
		{
			task.Assignees.ForEach(x => _userLinkService.AddSelfLink(x));
		}

		public virtual void AddSelfLink(Task task)
		{
			task.AddLink(GetSelfLink(task.TaskId.Value));
		}

		/// <summary>
		/// Uses the ICommonLinkService to build a link with the task name at the end
		/// </summary>
		/// <returns></returns>
		public virtual Link GetAllTasksLink()
		{
			const string pathFragment = "tasks";
			return _commonLinkService.GetLink(pathFragment, Constants.CommonLinkRelValues.All, HttpMethod.Get);
		}

		/// <summary>
		/// Uses the ICommonLinkService to build a link that points to a specific task
		/// </summary>
		/// <param name="taskId"></param>
		/// <returns></returns>
		public virtual Link GetSelfLink(long taskId)
		{
			var pathFragment = string.Format("tasks/{0}", taskId);
			var link = _commonLinkService.GetLink(pathFragment, Constants.CommonLinkRelValues.Self, HttpMethod.Get);
			return link;
		}
	}
}