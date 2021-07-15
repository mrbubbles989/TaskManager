using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Data.Entities
{
	public class User : IVersionedEntity
	{
		public virtual long UserId { get; set; }
		public virtual string FirstName { get; set; }
		public virtual string LastName { get; set; }
		public virtual string UserName { get; set; }
		public virtual byte[] Version { get; set; }

	}
}
