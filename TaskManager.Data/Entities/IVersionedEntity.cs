using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Data.Entities
{
	public interface IVersionedEntity
	{
		byte[] Version { get; set; }
	}
}
