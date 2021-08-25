using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Common.TypeMapping;
using TaskManager.Data.QueryProcessors;
using TaskManager.Data.Exceptions;
using TaskManager.Web.Api.Models;

namespace TaskManager.InquiryProcessing
{
	public class TaskByIdInquiryProcessor
	{
		private readonly IAutoMapper _autoMapper;
		private readonly ITaskByIdQueryProcessor _queryProcessor;

		public TaskByIdInquiryProcessor(ITaskByIdQueryProcessor queryProcessor, IAutoMapper autoMapper)
		{
			_queryProcessor = queryProcessor;
			_autoMapper = autoMapper;
		}
		
		public Task GetTask(long taskId)
		{
			var taskEntity = _queryProcessor.GetTask(taskId);

			if (taskEntity == null)
			{
				throw new RootObjectNotFoundException("Task not found");
			}

			var task = _autoMapper.Map<Task>(taskEntity);

			return task;
		}
	}
}