using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Data;
using TaskManager.Web.Api.Models;

namespace TaskManager.InquiryProcessing
{
	public interface IAllTasksInquiryProcessor
	{
		PagedDataInquiryResponse<Task> GetTasks(PagedDataRequest requestInfo);
	}
}
