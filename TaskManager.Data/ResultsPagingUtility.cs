using System;
using TaskManager.Common;

namespace TaskManager.Data
{
	/// <summary>
	/// The purpose of this class is to restrict page numbers and sizes to reasonable values.
	/// </summary>
	public static class ResultsPagingUtility
	{
		private const string ValueLessThanOneErrorMessage = "Value may not be less than 1.";
		private const string ValueLessThanZeroErrorMessage = "Value may not be less than 0.";

		/// <summary>
		/// 
		/// </summary>
		/// <param name="requestedValue"></param>
		/// <param name="maxValue"></param>
		/// <returns></returns>
		public static int CalculatePageSize(int requestedValue, int maxValue)
		{
			if (requestedValue < 1)
			{
				throw new ArgumentOutOfRangeException("requestedValue", requestedValue, ValueLessThanOneErrorMessage);
			}
			if (maxValue < 1)
			{
				throw new ArgumentOutOfRangeException("maxValue", maxValue, ValueLessThanOneErrorMessage);
			}
			var boundedPageSize = Math.Min(requestedValue, maxValue);
			return boundedPageSize;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pageNumber"></param>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		public static int CalculateStartIndex(int pageNumber, int pageSize)
		{
			if (pageNumber < 1)
			{
				throw new ArgumentOutOfRangeException(Constants.CommonParameterNames.PageNumber, pageNumber, ValueLessThanOneErrorMessage);
			}
			if (pageSize < 1)
			{
				throw new ArgumentOutOfRangeException(Constants.CommonParameterNames.PageSize, pageSize, ValueLessThanOneErrorMessage);
			}
			var startIndex = (pageNumber - 1) * pageSize;

			return startIndex;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="totalItemCount"></param>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		public static int CalculatePageCount(int totalItemCount, int pageSize)
		{
			if(totalItemCount < 0)
			{
				throw new ArgumentOutOfRangeException("totalItemCount", totalItemCount, ValueLessThanZeroErrorMessage);
			}
			if(pageSize < 1)
			{
				throw new ArgumentOutOfRangeException("pageSize", pageSize, ValueLessThanOneErrorMessage);
			}

			var totalPageCount = (totalItemCount + pageSize - 1) / pageSize;
			return totalPageCount;
		}
	}
}
