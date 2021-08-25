using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Data.Entities;

namespace TaskManager.Data.QueryProcessors
{
	public interface IAllTasksQueryProcessor
	{
		QueryResult<Task> GetTasks(PagedDataRequest requestInfo);
	}
}
