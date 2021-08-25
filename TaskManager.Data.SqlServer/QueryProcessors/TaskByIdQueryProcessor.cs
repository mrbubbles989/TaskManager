using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using TaskManager.Data.Entities;
using TaskManager.Data.QueryProcessors;

namespace TaskManager.Data.SqlServer.QueryProcessors
{
	public class TaskByIdQueryProcessor : ITaskByIdQueryProcessor
	{
		private readonly ISession _session;

		public TaskByIdQueryProcessor(ISession session)
		{
			_session = session;
		}

		public Task GetTask(long taskId)
		{
			var task = _session.Get<Task>(taskId);
			return task;
		}
	}
}
