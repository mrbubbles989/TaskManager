using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Tracing;
using TaskManager.Common.Logging;
using TaskManager.Web.Common;
using System.Web.Http.ExceptionHandling;
using TaskManager.Web.Common.ErrorHandling;

namespace TaskManager
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

			//config.EnableSystemDiagnosticsTracing(); //Replaced with a custom writer
			config.Services.Replace(typeof(ITraceWriter), new SimpleTraceWriter(WebContainerManager.Get<ILogManager>()));

			config.Services.Add(typeof(IExceptionLogger), new SimpleExceptionLogger(WebContainerManager.Get<ILogManager>()));

			config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
			
        }
    }
}
