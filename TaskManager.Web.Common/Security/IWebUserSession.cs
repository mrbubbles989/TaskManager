using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Common.Security;

namespace TaskManager.Web.Common.Security
{
	public interface IWebUserSession : IUserSession
	{
		string ApiVersionInUse { get; }
		Uri RequestUri { get; }
		string HttpRequestMethod { get; }
	}
}
