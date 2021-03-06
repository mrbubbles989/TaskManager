using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Common;
using TaskManager.Web.Api.Models;

namespace TaskManager.MaintenanceProcessing
{
	public static class LocationLinkCalculator
	{
		public static Uri GetLocationLink(ILinkContaining linkContaining)
		{
			var locationLink = linkContaining.Links.FirstOrDefault(x => x.Rel == Constants.CommonLinkRelValues.Self);
			return locationLink == null ? null : new Uri(locationLink.Href);
		}
	}
}