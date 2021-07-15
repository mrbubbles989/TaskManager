using System;

namespace TaskManager.Common
{
	public class DateTimeAdapter : IDateTime
	{
		public DateTime UtcNow
		{
			get { return DateTime.UtcNow; }
		}
	}
}
