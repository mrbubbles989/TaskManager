using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Common
{
	public interface IDateTime
	{
		DateTime UtcNow { get; }
	}
}
