using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using TaskManager.Web.Api.Models;

namespace TaskManager.LinkServices
{
	public interface ICommonLinkService
	{
		void AddPageLinks(IPageLinkContaining linkContainer,
			string currentPageQueryString,
			string previousPageQueryString,
			string nextPageQueryString);

		Link GetLink(string pathFragment, string relValue, HttpMethod httpMethod);
	}
}
