using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Data.Exceptions
{
	[Serializable]
	public class ChildObjectNotFoundException : Exception
	{
		public ChildObjectNotFoundException(string message) : base(message)
		{

		}
	}
}
