using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Common
{
	public class BusinessRuleViolationException : Exception
	{
		public BusinessRuleViolationException(string incorrectTaskStatus) : base(incorrectTaskStatus)
		{

		}
	}
}
