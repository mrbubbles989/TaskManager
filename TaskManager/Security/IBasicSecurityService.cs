﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Security
{
	public interface IBasicSecurityService
	{
		bool SetPrincipal(string username, string password);
	}
}
