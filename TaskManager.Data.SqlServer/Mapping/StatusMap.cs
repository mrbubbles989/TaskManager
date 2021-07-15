using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Data.Entities;

namespace TaskManager.Data.SqlServer.Mapping
{
	public class StatusMap : VersionedClassMap<Status>
	{
		public StatusMap()
		{
			Id(x => x.StatusId);
			Map(x => x.Name).Not.Nullable();
			Map(x => x.Ordinal).Not.Nullable();
		}
	}
}
