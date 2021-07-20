using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Common.Security
{
	public interface IUserSession
	{
		string Firstname { get; }
		string Lastname { get; }
		string Username { get; }
		bool IsInRole(string roleName);
	}
}
