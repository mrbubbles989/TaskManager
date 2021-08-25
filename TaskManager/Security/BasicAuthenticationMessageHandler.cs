using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using TaskManager.Common;
using TaskManager.Common.Logging;

namespace TaskManager.Security
{
	public class BasicAuthenticationMessageHandler : DelegatingHandler
	{
		public const char AuthorizationHeaderSeparator = ':';
		private const int UsernameIndex = 0;
		private const int PasswordIndex = 1;
		private const int ExpectedCredentialCount = 2;

		private readonly ILog _log;
		private readonly IBasicSecurityService _basicSecurityService;

		public BasicAuthenticationMessageHandler(ILogManager logManager, IBasicSecurityService basicSecurityService)
		{
			_basicSecurityService = basicSecurityService;
			_log = logManager.GetLog(typeof(BasicAuthenticationMessageHandler));
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (HttpContext.Current.User.Identity.IsAuthenticated)
			{
				_log.Debug("Already authenticated, passing on to the next handler... ");
				return await base.SendAsync(request, cancellationToken);
			}

			if (!CanHandleAuthentication(request))
			{
				_log.Debug("Not a basic auth request, passing on to the next handler... ");
				return await base.SendAsync(request, cancellationToken);
			}

			bool isAuthenticated;
			try
			{
				isAuthenticated = Authenticate(request);
			}
			catch (Exception e)
			{
				_log.Error("Failure in auth processing", e);
				return CreateUnauthorizedResponse();
			}

			if (isAuthenticated)
			{
				var response = await base.SendAsync(request, cancellationToken);
				return response.StatusCode == HttpStatusCode.Unauthorized ? CreateUnauthorizedResponse() : response;
			}

			return CreateUnauthorizedResponse();
		}

		/// <summary>
		/// Examines the request and returns true if it contains an HTTP header indicating the Basic authorization scheme
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public bool CanHandleAuthentication(HttpRequestMessage request)
		{
			return (request.Headers != null && request.Headers.Authorization != null && request.Headers.Authorization.Scheme.ToLowerInvariant() == Constants.SchemeTypes.Basic);
		}

		/// <summary>
		/// Uses GetCredentials ot extract the credentials from the request, and then it delegates the actual work of setting the principal to the security service we implemented earlier
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public bool Authenticate(HttpResponseMessage request)
		{
			_log.Debug("Attempting to authenticate... ");

			var authHeader = request.Headers.Authorization;
			if (authHeader == null)
			{
				return false;
			}

			var credentialParts = GetCredentialParts(authHeader);
			if (credentialParts.Length != ExpectedCredentialCount)
			{
				return false;
			}

			return _basicSecurityService.SetPrincipal(credentialParts[UsernameIndex], credentialParts[PasswordIndex]);
		}

		/// <summary>
		/// Parses the credentials from the request. //NTS: Remember the credentials arrive base64-encoded and seperated by a ":"
		/// </summary>
		/// <param name="authHeader"></param>
		/// <returns></returns>
		public string[] GetCredentialParts(AuthenticationHeaderValue authHeader)
		{
			var encodedCredentials = authHeader.Parameter;
			var credentialBytes = Convert.FromBase64String(encodedCredentials);
			var credentials = Encoding.ASCII.GetString(credentialBytes);
			var credentialParts = credentials.Split(AuthorizationHeaderSeparator);
			return credentialParts;
		}

		/// <summary>
		/// If the user is not validated this will trigger most browsers to prompt for Username and Password
		/// </summary>
		/// <returns></returns>
		public HttpResponseMessage CreateUnauthorizedResponse()
		{
			var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
			response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(Constants.SchemeTypes.Basic));
			return response;
		}
	}
}