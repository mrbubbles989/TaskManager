using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Data.Entities;

namespace TaskManager.Data.QueryProcessors
{
	public interface IUpdateTaskStatusQueryProcessor
	{
		void UpdateTaskStatus(Task taskToUpdate, string statusName);
	}
}
