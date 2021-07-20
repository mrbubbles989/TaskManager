using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Web.Api.Models
{
	public class NewTaskV2
	{
		public string Subject { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? DueDate { get; set; }
		public User Assignee { get; set; }
	}
}
