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
using TaskManager.Common.Security;
using TaskManager.Web.Common.Security;
using TaskManager.Data.SqlServer.QueryProcessors;
using TaskManager.Data.QueryProcessors;
using TaskManager.Common.TypeMapping;
using TaskManager.AutoMappingConfiguration;
using TaskManager.MaintenanceProcessing;
using TaskManager.Security;
using TaskManager.InquiryProcessing;
using TaskManager.LinkServices;

namespace TaskManager.App_Start
{
	/// <summary>
	/// Sets up the Ninject DI container
	/// </summary>
	public class NinjectConfigurator
	{

		/// <summary>
		/// Entry method used by caller to configure the given container with all of this applications dependencies
		/// </summary>
		/// <param name="container"></param>
		public void Configure(IKernel container)
		{
			AddBindings(container);
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

		private void ConfigureUserSession(IKernel container)
		{
			var userSession = new UserSession();
			container.Bind<IUserSession>().ToConstant(userSession).InSingletonScope();
			container.Bind<IWebUserSession>().ToConstant(userSession).InSingletonScope();
		}

		private void AddBindings(IKernel container)
		{
			ConfigureLog4net(container);
			ConfigureUserSession(container);
			ConfigureNHibernate(container);
			ConfigureAutoMapper(container);

			container.Bind<IDateTime>().To<DateTimeAdapter>().InSingletonScope();
			container.Bind<IAddTaskQueryProcessor>().To<AddTaskQueryProcessor>().InRequestScope();
			container.Bind<IAddTaskMaintenanceProcessor>().To<AddTaskMaintenanceProcessor>().InRequestScope();
			container.Bind<IBasicSecurityService>().To<BasicSecurityService>().InSingletonScope();
			container.Bind<ITaskByIdQueryProcessor>().To<TaskByIdQueryProcessor>().InRequestScope();
			container.Bind<IUpdateTaskStatusQueryProcessor>().To<UpdateTaskStatusQueryProcessor>().InRequestScope();
			container.Bind<IStartTaskWorkflowProcessor>().To<StartTaskWorkflowProcessor>().InRequestScope();
			container.Bind<ICompleteTaskWorkflowProcessor>().To<CompleteTaskWorkflowProcessor>().InRequestScope();
			container.Bind<IReactivateTaskWorkflowProcessor>().To<ReactivateTaskWorkflowProcessor>().InRequestScope();
			container.Bind<ITaskByIdQueryProcessor>().To<TaskByIdQueryProcessor>().InRequestScope();
			container.Bind<IUpdateTaskQueryProcessor>().To<UpdateTaskQueryProcessor>().InRequestScope();
			container.Bind<ITaskUsersMaintenanceProcessor>().To<TaskUsersMaintenanceProcessor>().InRequestScope();
			container.Bind<IUpdateablePropertyDetector>().To<JObjectUpdateablePropertyDetector>().InSingletonScope();
			container.Bind<IUpdateTaskMaintenanceProcessor>().To<UpdateTaskMaintenanceProcessor>().InRequestScope();
			container.Bind<IPagedDataRequestFactory>().To<PagedDataRequestFactory>().InSingletonScope();
			container.Bind<IAllTasksQueryProcessor>().To<AllTasksQueryProcessor>().InRequestScope();
			container.Bind<IAllTasksInquiryProcessor>().To<AllTasksInquiryProcessor>().InRequestScope();
			container.Bind<ICommonLinkService>().To<CommonLinkService>().InRequestScope();
			container.Bind<IUserLinkService>().To<UserLinkService>().InRequestScope();
			container.Bind<ITaskLinkService>().To<TaskLinkService>().InRequestScope();

		}

		private void ConfigureAutoMapper(IKernel container)
		{
			container.Bind<IAutoMapper>().To<AutoMapperAdapter>().InSingletonScope();

			container.Bind<IAutoMapperTypeConfigurator>().To<StatusEntityToStatusAutoMapperTypeConfigurator>().InSingletonScope();

			container.Bind<IAutoMapperTypeConfigurator>().To<StatusToStatusEntityAutoMapperTypeConfigurator>().InSingletonScope();
			 
			container.Bind<IAutoMapperTypeConfigurator>().To<UserEntityToUserAutoMapperTypeConfigurator>().InSingletonScope();

			container.Bind<IAutoMapperTypeConfigurator>().To<UserToUserEntityAutoMapperTypeConfigurator>().InSingletonScope();

			container.Bind<IAutoMapperTypeConfigurator>().To<NewTaskToTaskEntityAutoMapperTypeConfigurator>().InSingletonScope();

			container.Bind<IAutoMapperTypeConfigurator>().To<TaskEntityToTaskAutoMapperTypeConfigurator>().InSingletonScope();
		}
	}
}