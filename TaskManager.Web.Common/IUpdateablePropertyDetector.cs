using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Web.Common
{
	public interface IUpdateablePropertyDetector
	{
		IEnumerable<string> GetNamesOfPropertiesToUpdate<TTargetType>(object objectContainingUpdateData);
	}
}
