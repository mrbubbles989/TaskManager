using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Data.Entities;

namespace TaskManager.Data.QueryProcessors
{
	public interface IAddTaskQueryProcessor
	{
		void AddTask(Task task);
	}
}
