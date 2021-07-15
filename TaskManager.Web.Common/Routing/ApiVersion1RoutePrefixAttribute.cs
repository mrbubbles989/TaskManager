using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http;

namespace TaskManager.Web.Common.Routing
{
	public class ApiVersion1RoutePrefixAttribute : RoutePrefixAttribute
	{
		private const string RouteBase = "api/{apiVersion:apiVersionConstraint(v1)}";
		private const string PrefixRouteBase = RouteBase + "/";
		public ApiVersion1RoutePrefixAttribute(string routePrefix) : base(string.IsNullOrWhiteSpace(routePrefix) ? RouteBase : PrefixRouteBase + routePrefix)
		{

		}
	}
}
