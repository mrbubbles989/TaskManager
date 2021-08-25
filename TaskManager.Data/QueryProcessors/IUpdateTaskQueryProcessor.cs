using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Data.Entities;
using PropertyValueMapType = System.Collections.Generic.Dictionary<string, object>;

namespace TaskManager.Data.QueryProcessors
{
	public interface IUpdateTaskQueryProcessor
	{
		Task GetUpdatedTask(long taskId, PropertyValueMapType updatedPropertyValueMap);
		Task ReplaceTaskUsers(long taskId, IEnumerable<long> userIds);
		Task DeleteTaskUsers(long taskId);
		Task AddTaskUser(long taskId, long userId);
		Task DeleteTaskUser(long taskId, long userId);
	}
}
