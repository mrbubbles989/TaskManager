using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Web.Api.Models;

namespace TaskManager.InquiryProcessing
{
	public interface ITaskByIdInquiryProcessor
	{
		Task GetTask(long taskId);
	}
}
