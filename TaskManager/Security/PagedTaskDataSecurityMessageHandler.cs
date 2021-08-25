using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using TaskManager.Common;
using TaskManager.Common.Logging;
using TaskManager.Common.Security;
using TaskManager.Web.Api.Models;
using Task = TaskManager.Web.Api.Models.Task;

namespace TaskManager.Security
{
	public class PagedTaskDataSecurityMessageHandler : DelegatingHandler
	{
		private readonly ILog _log;
		private readonly IUserSession _userSession;

		public PagedTaskDataSecurityMessageHandler(ILogManager logManager, IUserSession userSession)
		{
			_log = logManager.GetLog(typeof(PagedTaskDataSecurityMessageHandler));
			_userSession = userSession;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var response = await base.SendAsync(request, cancellationToken);

			if (CanHandleResponse(response))
			{
				ApplySecurityToResponseData((ObjectContent)response.Content);
			}

			return response;
		}

		public bool CanHandleResponse(HttpResponseMessage response)
		{
			var objectContent = response.Content as ObjectContent;
			var canHandleResponse = objectContent != null && objectContent.ObjectType == typeof(PagedDataInquiryResponse<Task>);
			return canHandleResponse;
		}

		public void ApplySecurityToResponseData(ObjectContent responseObjectContent)
		{
			var maskData = !_userSession.IsInRole(Constants.RoleNames.SeniorWorker);

			if (maskData)
			{
				_log.DebugFormat("Applying security masking for user {0}", _userSession.Username);
			}

			((PagedDataInquiryResponse<Task>)responseObjectContent.Value).Items.ForEach(x => x.SetShouldSerializeAssigness(!maskData));
		}
	}
}