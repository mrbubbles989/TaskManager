using System.Collections.Generic;
using System.Linq;
using TaskManager.Common.TypeMapping;
using TaskManager.Data;
using TaskManager.Data.QueryProcessors;
using TaskManager.Web.Api.Models;
using PagedTaskDataInquiryResponse = TaskManager.Web.Api.Models.PagedDataInquiryResponse<TaskManager.Web.Api.Models.Task>;
using TaskManager.LinkServices;

namespace TaskManager.InquiryProcessing
{
	public class AllTasksInquiryProcessor : IAllTasksInquiryProcessor
	{
		public const string QueryStringFormat = "pagenumber={0}&pagesize={1}";

		private readonly IAutoMapper _autoMapper;
		private readonly IAllTasksQueryProcessor _queryProcessor;
		private readonly ICommonLinkService _commonLinkService;
		private readonly ITaskLinkService _taskLinkService;

		public AllTasksInquiryProcessor(IAllTasksQueryProcessor queryProcessor, IAutoMapper autoMapper, ICommonLinkService commonLinkService, ITaskLinkService taskLinkService)
		{
			_queryProcessor = queryProcessor;
			_autoMapper = autoMapper;
			_commonLinkService = commonLinkService;
			_taskLinkService = taskLinkService;
		}

		public PagedTaskDataInquiryResponse GetTasks(PagedDataRequest requestInfo)
		{
			var queryResult = _queryProcessor.GetTasks(requestInfo);

			var tasks = GetTasks(queryResult.QueriedItems).ToList();

			var inquiryResponse = new PagedTaskDataInquiryResponse
			{
				Items = tasks,
				PageCount = queryResult.TotalPageCount,
				PageNumber = requestInfo.PageNumber,
				PageSize = requestInfo.PageSize
			};

			AddLinksToInquiryResponse(inquiryResponse); 

			return inquiryResponse;
		}

		public virtual void AddLinksToInquiryResponse(PagedTaskDataInquiryResponse inquiryResponse)
		{
			inquiryResponse.AddLink(_taskLinkService.GetAllTasksLink());

			_commonLinkService.AddPageLinks(inquiryResponse, GetCurrentPageQueryString(inquiryResponse), GetPreviousPageQueryString(inquiryResponse), GetNextPageQueryString(inquiryResponse));
		}

		public virtual IEnumerable<Task> GetTasks(IEnumerable<Data.Entities.Task> taskEntities)
		{
			var tasks = taskEntities.Select(x => _autoMapper.Map<Task>(x)).ToList();

			tasks.ForEach(x =>
			{
				_taskLinkService.AddSelfLink(x);
				_taskLinkService.AddLinksToChildObjects(x);
			});

			return tasks;
		}

		public virtual string GetCurrentPageQueryString(PagedTaskDataInquiryResponse inquiryResponse)
		{
			return string.Format(QueryStringFormat, inquiryResponse.PageNumber, inquiryResponse.PageSize);
		}

		public virtual string GetPreviousPageQueryString(PagedTaskDataInquiryResponse inquiryResponse)
		{
			return string.Format(QueryStringFormat, inquiryResponse.PageNumber - 1, inquiryResponse.PageSize);
		}

		public virtual string GetNextPageQueryString(PagedTaskDataInquiryResponse inquiryResponse)
		{
			return string.Format(QueryStringFormat, inquiryResponse.PageNumber + 1, inquiryResponse.PageSize);
		}
	}
}