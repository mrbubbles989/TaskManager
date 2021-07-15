using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Web.Api.Models
{
	public class Status
	{
		public long StatusId { get; set; }
		public string Name { get; set; }
		public int Ordinal { get; set; } 
	}
}
