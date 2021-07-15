[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TaskManager.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(TaskManager.App_Start.NinjectWebCommon), "Stop")]

namespace TaskManager.App_Start
{
    using System;
    using System.Web;
	using System.Web.Http;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
	using WebActivatorEx;
	using TaskManager.Web.Api;
	using TaskManager.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
		/// Modifications: Modified the Start method to register the dependency resolver with the Web Api Configuration. 
		/// In doing so, directed the framework to hit the configured Ninject container instance to resolve any dependencies that are needed.
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
			IKernel container = null;
            bootstrapper.Initialize(() =>
			{
				container = CreateKernel();
				return container;
			});

			var resolver = new NinjectDependencyResolver(container);
			GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
		/// Modifications: the method configures the container bindings using the NinjectConfigurator class. (Set up in TaskManager.Web.Api.App_Start.NinjectConfigurator)
        private static void RegisterServices(IKernel kernel)
        {
			var containerConfigurator = new NinjectConfigurator();
			containerConfigurator.Configure(kernel);
        }
    }
}