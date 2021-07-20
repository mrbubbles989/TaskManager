using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Data.Exceptions
{
	[Serializable]
	public class RootObjectNotFoundException : Exception
	{
		public RootObjectNotFoundException(string message) : base(message)
		{

		}
	}
}
