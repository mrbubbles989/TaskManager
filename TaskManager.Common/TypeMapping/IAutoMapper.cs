using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Common.TypeMapping
{
	public interface IAutoMapper
	{
		T Map<T>(object objectToMap);
	}
}
