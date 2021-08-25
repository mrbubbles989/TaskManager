using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Web.Api.Models;

namespace TaskManager.MaintenanceProcessing
{
	public interface ICompleteTaskWorkflowProcessor
	{
		Task CompleteTask(long taskId);
	}
}
