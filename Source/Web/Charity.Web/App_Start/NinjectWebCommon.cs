[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Charity.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Charity.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Charity.Web.App_Start
{
    using System;
    using System.Data.Entity;
    using System.Security.Principal;
    using System.Web;
    using Charity.Data;
    using Charity.Data.Common;
    using Charity.Data.Common.Repositories;
    using Charity.Data.Repositories;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Charity.Web.Infrastructure.Identity;

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
            kernel.Bind<DbContext>().To<ApplicationDbContext>().InSingletonScope();

            kernel.Bind<IApplicationDbContext>().To<ApplicationDbContext>().InSingletonScope();

            kernel.Bind<IIdentity>().ToMethod(c => HttpContext.Current.User.Identity);

            kernel.Bind<ICurrentUser>().To<CurrentUser>().InSingletonScope();

            kernel.Bind(typeof(IDeletableEntityRepository<>))
            .To(typeof(DeletableEntityRepository<>)).InSingletonScope();

            kernel.Bind(typeof(IRepository<>)).To(typeof(GenericRepository<>)).InSingletonScope();

            kernel.Bind<ICityRepository>().To<CityRepository>().InSingletonScope();

            kernel.Bind<IApplicationUserRepository>().To<ApplicationUserRepository>().InSingletonScope();

            kernel.Bind<IAdministratorRepository>().To<AdministratorRepository>().InSingletonScope();

            kernel.Bind<IDonorRepository>().To<DonorRepository>().InSingletonScope();

            kernel.Bind<IRecipientTypeRepository>().To<RecipientTypeRepository>().InSingletonScope();
            kernel.Bind<IRecipientRepository>().To<RecipientRepository>().InSingletonScope();

            kernel.Bind<IFoodCategoryRepository>().To<FoodCategoryRepository>().InSingletonScope();

            kernel.Bind<IFoodDonationRepository>().To<FoodDonationRepository>().InSingletonScope();
            kernel.Bind<IFoodDonationCommentRepository>().To<FoodDonationCommentRepository>().InSingletonScope();

            kernel.Bind<IFoodRequestRepository>().To<FoodRequestRepository>().InSingletonScope();
            kernel.Bind<IFoodRequestCommentRepository>().To<FoodRequestCommentRepository>().InSingletonScope();
        }        
    }
}
