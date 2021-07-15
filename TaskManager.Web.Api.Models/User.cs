using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Web.Api.Models
{
	public class User
	{
		private List<Link> _links;

		public long UserId { get; set; }
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public List<Link> Links
		{
			get { return _links ?? (_links = new List<Link>()); }
			set { _links = value; }
		}

		public void AddLink(Link link)
		{
			Links.Add(link);
		}
	}
}
