using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using log4net;
using NHibernate;
using TaskManager.Common;
using TaskManager.Common.Logging;
using TaskManager.Data.Entities;
using TaskManager.Web.Common;

namespace TaskManager.Security
{
	public class BasicSecurityService : IBasicSecurityService
	{
		private readonly ILog _log;

		public BasicSecurityService(ILogManager logManager)
		{
			_log = logManager.GetLog(typeof(BasicSecurityService));
		}

		public virtual ISession Session
		{
			get { return WebContainerManager.Get<ISession>(); }
		}

		public bool SetPrincipal(string username, string password)
		{
			var user = GetUser(username);

			IPrincipal principal = null;
			if (user == null || (principal = GetPrincipal(user)) == null)
			{
				_log.DebugFormat("System could not validate user {0}", username);
				return false;
			}

			Thread.CurrentPrincipal = principal;
			if (HttpContext.Current != null)
			{
				HttpContext.Current.User = principal;
			}
			return true;
		}

		public virtual IPrincipal GetPrincipal(User user)
		{
			var identity = new GenericIdentity(user.UserName, Constants.SchemeTypes.Basic);

			identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));
			identity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));

			var username = user.UserName.ToLowerInvariant();

			switch (username)
			{
				case "bhogg":
					identity.AddClaim(new Claim(ClaimTypes.Role, Constants.RoleNames.Manager));
					identity.AddClaim(new Claim(ClaimTypes.Role, Constants.RoleNames.SeniorWorker));
					identity.AddClaim(new Claim(ClaimTypes.Role, Constants.RoleNames.JuniorWorker));
					break;
				case "jbob":
					identity.AddClaim(new Claim(ClaimTypes.Role, Constants.RoleNames.SeniorWorker));
					identity.AddClaim(new Claim(ClaimTypes.Role, Constants.RoleNames.JuniorWorker));
					break;
				case "jdoe":
					identity.AddClaim(new Claim(ClaimTypes.Role, Constants.RoleNames.JuniorWorker));
					break;
				default:
					return null;
			}

			return new ClaimsPrincipal(identity);
		}

		public virtual User GetUser(string username)
		{
			username = username.ToLowerInvariant();
			return
				Session.QueryOver<User>().Where(x => x.UserName == username).SingleOrDefault();
		}
	}
}