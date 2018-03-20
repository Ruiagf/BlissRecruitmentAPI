[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(BlissRecruitment.Infrastructure.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(BlissRecruitment.Infrastructure.NinjectWebCommon), "Stop")]

namespace BlissRecruitment.Infrastructure
{
    using AutoMapper;
    using BlissRecruitment.BusinessLogic.Abstract;
    using BlissRecruitment.BusinessLogic.Concrete;
    using BlissRecruitment.DataAccess.Abstract;
    using BlissRecruitment.DataAccess.Concrete;
    using BlissRecruitment.Domain.Utils;
    using BlissRecruitment.Infrastructure.Email;
    using global::AutoMapper;
    using log4net;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Activation;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using System;
    using System.Configuration;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

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
                RegisterServices(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            kernel.Bind<ILog>().ToMethod(context => LogManager.GetLogger(GetParentTypeName(context)));

            kernel.Bind<IHealthStatusProvider>().To<HealthStatusProvider>();
            kernel.Bind<IQuestionProvider>().To<QuestionProvider>();
            kernel.Bind<IEmailProvider>().To<EmailProvider>();

            kernel.Bind<IShareProvider>().To<ShareProvider>().WithConstructorArgument("configuration", new ShareConfiguration {
                 FromDescription = "Bliss Applications",
                 Subject = "Someone wants to share a link with you",
                 Body = @"<body>
    <h2>Recruitment API</h2>

    <p></p>

    <p>
        Hi,
    </p>

    <p>
        Someone wants to share with you the following URL: <a href='{URL}' target='_blank\'>{URL}</a>
    </p>

    <p></p>


    <p>Kind regards,</p>

    <p>Bliss Recruitment API</p>

    <small>Powered by Rui Freitas</small>
</body>"});

            kernel.Bind<IHealthStatusRepository>().To<HealthStatusRepository>().WithConstructorArgument("connectionString", connectionString);
            kernel.Bind<IQuestionRepository>().To<QuestionRepository>().WithConstructorArgument("connectionString", connectionString);

            var mapperConfig = new MapperConfiguration(config => config.AddProfile<AutoMapperProfile>());
            mapperConfig.AssertConfigurationIsValid();

            kernel.Bind<IMapper>().ToConstant(mapperConfig.CreateMapper());
        }

        private static string GetParentTypeName(IContext context)
        {
            return context.Request.ParentContext.Request.Service.FullName;
        }
    }
}
