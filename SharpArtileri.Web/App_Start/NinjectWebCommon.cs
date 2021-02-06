using System.Configuration;
using System.Security.Principal;
using System.Web.Security;
using Ninject.Parameters;
using SharpArtileri.Data;
using SharpArtileri.Services;
using SharpArtileri.Web.Base;
using SharpArtileri.Web.Helpers;

[assembly: WebActivator.PreApplicationStartMethod(typeof(SharpArtileri.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(SharpArtileri.Web.App_Start.NinjectWebCommon), "Stop")]

namespace SharpArtileri.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
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
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ManagementDataContext>()
                  .ToConstructor(
                      o =>
                          new ManagementDataContext(
                              ConfigurationManager.ConnectionStrings["ManagementConnectionString"].ConnectionString))
                  .InRequestScope();
                  ////.WhenInjectedInto<string>()                  
                  //.WithParameter(new ConstructorArgument("connection",                      
                  //    ConfigurationManager.ConnectionStrings["ManagementConnectionString"].ConnectionString
                  //    ));

            kernel.Bind<IPrincipal>().ToMethod(ctx => HttpContext.Current.User);            

            kernel.Bind<ArtileriDataContext>()
                  .ToMethod(ctx => new ArtileriDataContext(ctx.Kernel.Get<ManagementProvider>()
                                                              .GetConnectionString(HttpContext.Current ==
                                                                                              null
                                                                  ? ""
                                                                  : UserSessionHelper.GetCurrentCompanyCode())))
                  .InRequestScope();
        }        
    }
}
