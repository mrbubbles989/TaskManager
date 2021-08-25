using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Web.Api.Models;

namespace TaskManager.LinkServices
{
	public interface ITaskLinkService 
	{
		Link GetAllTasksLink();
		void AddSelfLink(Task task);
		void AddLinksToChildObjects(Task task);
	}
}
