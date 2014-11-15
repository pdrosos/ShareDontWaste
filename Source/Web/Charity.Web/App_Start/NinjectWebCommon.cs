[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Charity.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Charity.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Charity.Web.App_Start
{
    using System;
    using System.Security.Principal;
    using System.Web;
    using Charity.Data;
    using Charity.Data.Common;
    using Charity.Data.Common.Repositories;
    using Charity.Data.Repositories;
    using Charity.Services;
    using Charity.Services.Common;
    using Charity.Web.Infrastructure.Identity;
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
        private static void RegisterServices(IKernel kernel)
        {
            RegisterDbContext(kernel);

            RegisterIdentity(kernel);
            
            RegisterRepositories(kernel);

            RegisterApplicationServices(kernel);
        }

        private static void RegisterDbContext(IKernel kernel)
        {
            //kernel.Bind<ApplicationDbContext>().ToSelf().InRequestScope();
            //kernel.Bind<DbContext>().To<ApplicationDbContext>();

            kernel.Bind<IApplicationDbContext>().To<ApplicationDbContext>().InRequestScope();
        }

        private static void RegisterIdentity(IKernel kernel)
        {
            kernel.Bind<IIdentity>().ToMethod(c => HttpContext.Current.User.Identity);

            kernel.Bind<ICurrentUser>().To<CurrentUser>();
        }

        private static void RegisterRepositories(IKernel kernel)
        {
            kernel.Bind(typeof(IRepository<>)).To(typeof(GenericRepository<>));

            kernel.Bind(typeof(IDeletableEntityRepository<>)).To(typeof(DeletableEntityRepository<>));

            kernel.Bind<ICityRepository>().To<CityRepository>();

            kernel.Bind<IApplicationUserRepository>().To<ApplicationUserRepository>();

            kernel.Bind<IAdministratorRepository>().To<AdministratorRepository>();

            kernel.Bind<IDonorRepository>().To<DonorRepository>();

            kernel.Bind<IRecipientTypeRepository>().To<RecipientTypeRepository>();
            kernel.Bind<IRecipientRepository>().To<RecipientRepository>();

            kernel.Bind<IFoodCategoryRepository>().To<FoodCategoryRepository>();

            kernel.Bind<IFoodDonationRepository>().To<FoodDonationRepository>();

            kernel.Bind<IFoodRequestRepository>().To<FoodRequestRepository>();
            kernel.Bind<IFoodRequestCommentRepository>().To<FoodRequestCommentRepository>();
        }

        private static void RegisterApplicationServices(IKernel kernel)
        {
            kernel.Bind<ICityService>().To<CityService>();

            kernel.Bind<IDonorProfileService>().To<DonorProfileService>();

            kernel.Bind<IRecipientTypeService>().To<RecipientTypeService>();
            kernel.Bind<IRecipientProfileService>().To<RecipientProfileService>();

            kernel.Bind<IFoodCategoryService>().To<FoodCategoryService>();

            kernel.Bind<IFoodDonationService>().To<FoodDonationService>();

            kernel.Bind<IFoodRequestService>().To<FoodRequestService>();
            kernel.Bind<IFoodRequestCommentService>().To<FoodRequestCommentService>();
        }
    }
}
