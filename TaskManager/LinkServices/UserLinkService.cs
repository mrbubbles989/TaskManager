using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using TaskManager.Common;
using TaskManager.Web.Api.Models;

namespace TaskManager.LinkServices
{
	public class UserLinkService : IUserLinkService
	{
		private readonly ICommonLinkService _commonLinkService;

		public UserLinkService(ICommonLinkService commonLinkService)
		{
			_commonLinkService = commonLinkService;
		}

		public virtual void AddSelfLink(User user)
		{
			user.AddLink(GetSelfLink(user));
		}

		public virtual Link GetSelfLink(User user)
		{
			var pathFragment = string.Format("users/{0}", user.UserId);
			var link = _commonLinkService.GetLink(pathFragment, Constants.CommonLinkRelValues.Self, HttpMethod.Get);
			return link;
		}
	}
}