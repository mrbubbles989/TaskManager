using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Web.Api.Models;

namespace TaskManager.LinkServices
{
	public interface IUserLinkService
	{
		void AddSelfLink(User user);
	}
}
