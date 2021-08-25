using System;
using TaskManager.Data;

namespace TaskManager.InquiryProcessing
{
	public interface IPagedDataRequestFactory
	{
		PagedDataRequest Create(Uri requestUri);
	};
}
