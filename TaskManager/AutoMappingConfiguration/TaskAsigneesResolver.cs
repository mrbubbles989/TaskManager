using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Data.Entities;
using TaskManager.Web.Common;
using User = TaskManager.Web.Api.Models.User;

namespace TaskManager.AutoMappingConfiguration
{
	public class TaskAsigneesResolver : ValueResolver<Task, List<User>>
	{
		public IAutoMapper AutoMapper
		{
			get { return WebContainerManager.Get<IAutoMapper>(); }
		}

		protected override List<User> ResolveCore(Task source)
		{
			return source.Users.Select(x => AutoMapper.Map<User>(x)).ToList();
		}
	}
}