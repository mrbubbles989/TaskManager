using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Data.Entities;

namespace TaskManager.Data.SqlServer.Mapping
{
	public class UserMap : VersionedClassMap<User>
	{
		public UserMap()
		{
			Id(x => x.UserId);
			Map(x => x.FirstName).Not.Nullable();
			Map(x => x.LastName).Not.Nullable();
			Map(x => x.UserName).Not.Nullable();
		}
	}
}
