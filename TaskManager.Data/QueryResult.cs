using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Data
{
	/// <summary>
	/// The purpose of this class is to serve as a paging-enhanced data-transfer object that is used to encapsulate data returned by the query processor.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class QueryResult<T>
	{
		public QueryResult(IEnumerable<T> queriedItems, int totalItemCount, int pageSize)
		{
			PageSize = pageSize;
			TotalItemCount = totalItemCount;
			QueriedItems = queriedItems ?? new List<T>();
		}

		public int TotalItemCount { get; private set; }

		public int TotalPageCount
		{
			get { return ResultsPagingUtility.CalculatePageCount(TotalItemCount, PageSize); }
		}

		public IEnumerable<T> QueriedItems { get; private set; }

		public int PageSize { get; private set; }
	}
}
