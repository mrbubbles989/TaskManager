using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net.Config;
using Ninject;
using TaskManager.Common;
using TaskManager.Common.Logging;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Context;
using Ninject.Activation;
using Ninject.Web.Common;
using TaskManager.Data.SqlServer.Mapping;
using TaskManager.Web.Common;

namespace TaskManager.App_Start
{
	public class NinjectConfigurator
	{

		public void Configure(IKernel container)
		{
			AddBindings(container);
		}

		public void AddBindings(IKernel container)
		{
			ConfigureLog4net(container);
			ConfigureNHibernate(container);

			container.Bind<IDateTime>().To<DateTimeAdapter>().InSingletonScope();
		}

		public void ConfigureLog4net(IKernel container)
		{
			XmlConfigurator.Configure();

			var logManager = new LogManagerAdapter();
			container.Bind<ILogManager>().ToConstant(logManager);
		}

		/// <summary>
		/// Sets four properties and then builds the ISessionFactory
		/// </summary>
		/// <param name="container"></param>
		private void ConfigureNHibernate(IKernel container)
		{
			var sessionFactory = Fluently.Configure()
				.Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("TaskManagerDb")))
				.CurrentSessionContext("web")
				.Mappings(m => m.FluentMappings.AddFromAssemblyOf<TaskMap>())
				.BuildSessionFactory();

			container.Bind<ISessionFactory>().ToConstant(sessionFactory);
			container.Bind<ISession>().ToMethod(CreateSession).InRequestScope();
			container.Bind<IActionTransactionHelper>().To<ActionTransactionHelper>().InRequestScope();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		private ISession CreateSession(IContext context)
		{
			var sessionFactory = context.Kernel.Get<ISessionFactory>();
			if (!CurrentSessionContext.HasBind(sessionFactory))
			{
				var session = sessionFactory.OpenSession();
				CurrentSessionContext.Bind(session);
			}

			return sessionFactory.GetCurrentSession();
		}


	}
}