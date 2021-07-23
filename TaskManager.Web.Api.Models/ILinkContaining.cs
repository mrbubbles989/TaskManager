using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Web.Api.Models
{
	public interface ILinkContaining
	{
		List<Link> Links { get; set; }
		void AddLink(Link link);
	}
}
