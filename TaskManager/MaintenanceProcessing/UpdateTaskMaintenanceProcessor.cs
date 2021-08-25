using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using TaskManager.Common.TypeMapping;
using TaskManager.Data.QueryProcessors;
using TaskManager.Web.Api.Models;
using TaskManager.Web.Common;
using PropertyValueTypeMap = System.Collections.Generic.Dictionary<string, object>;

namespace TaskManager.MaintenanceProcessing
{
	public class UpdateTaskMaintenanceProcessor : IUpdateTaskMaintenanceProcessor
	{
		private readonly IAutoMapper _autoMapper;
		private readonly IUpdateTaskQueryProcessor _queryProcessor;
		private readonly IUpdateablePropertyDetector _updateablePropertyDetector;

		public UpdateTaskMaintenanceProcessor(IUpdateTaskQueryProcessor queryProcessor, IAutoMapper autoMapper, IUpdateablePropertyDetector updateablePropertyDetector)
		{
			_queryProcessor = queryProcessor;
			_updateablePropertyDetector = updateablePropertyDetector;
			_autoMapper = autoMapper;
		}

		public Task UpdateTask(long taskId, object taskFragment)
		{
			var taskFragmentAsJObject = (JObject)taskFragment;
			var taskContainingUpdateData = taskFragmentAsJObject.ToObject<Task>();

			var updatedPropertyValueMap = GetPropertyValueMap(taskFragmentAsJObject, taskContainingUpdateData);

			var updatedTaskEntity = _queryProcessor.GetUpdatedTask(taskId, updatedPropertyValueMap);

			var task = _autoMapper.Map<Task>(updatedTaskEntity);

			return task;
		}

		public virtual PropertyValueTypeMap GetPropertyValueMap(JObject taskFragment, Task taskContainingUpdateData)
		{
			var namesOfModifiedProperties = _updateablePropertyDetector.GetNamesOfPropertiesToUpdate<Task>(taskFragment).ToList();

			var propertyInfos = typeof(Task).GetProperties();
			var updatedPropertyValueMap = new PropertyValueTypeMap();

			foreach (var propertyName in namesOfModifiedProperties)
			{
				var propertyValue = propertyInfos.Single(x => x.Name == propertyName).GetValue(taskContainingUpdateData);
				updatedPropertyValueMap.Add(propertyName, propertyValue);
			}

			return updatedPropertyValueMap;
		}
	}
}