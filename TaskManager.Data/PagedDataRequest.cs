using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Data
{
	public class PagedDataRequest
	{
		public int PageNumber { get; private set; }
		public int PageSize { get; private set; }
		public bool ExcludeLinks { get; set; }
		public PagedDataRequest(int pageNumber, int pageSize)
		{
			PageNumber = pageNumber;
			PageSize = pageSize;
		}
	}
}
