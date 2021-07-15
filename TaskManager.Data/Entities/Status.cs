using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Data.Entities
{
	public class Status : IVersionedEntity
	{
		public virtual long StatusId { get; set; }
		public virtual string Name { get; set; }
		public virtual int Ordinal { get; set; }
		public virtual byte[] Version { get; set; }
	}
}
