[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Charity.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Charity.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Charity.Web.App_Start
{
    using System;
    using System.Security.Principal;
    using System.Web;
    using Charity.Data;
    using Charity.Data.Common.Repositories;
    using Charity.Data.Repositories;
    using Charity.Web.Infrastructure.Identity;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using System.Data.Entity;
    using Charity.Data.Common;

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
            //kernel.Bind<ApplicationDbContext>().ToSelf().InRequestScope();

            kernel.Bind<IApplicationDbContext>().To<ApplicationDbContext>().InRequestScope();

            kernel.Bind<IIdentity>().ToMethod(c => HttpContext.Current.User.Identity);

            kernel.Bind<ICurrentUser>().To<CurrentUser>();

            kernel.Bind(typeof(IDeletableEntityRepository<>))
            .To(typeof(DeletableEntityRepository<>));

            kernel.Bind(typeof(IRepository<>)).To(typeof(GenericRepository<>));

            kernel.Bind<ICityRepository>().To<CityRepository>();

            kernel.Bind<IApplicationUserRepository>().To<ApplicationUserRepository>();

            kernel.Bind<IAdministratorRepository>().To<AdministratorRepository>();

            kernel.Bind<IDonorRepository>().To<DonorRepository>();

            kernel.Bind<IRecipientTypeRepository>().To<RecipientTypeRepository>();
            kernel.Bind<IRecipientRepository>().To<RecipientRepository>();

            kernel.Bind<IFoodCategoryRepository>().To<FoodCategoryRepository>();

            kernel.Bind<IFoodDonationRepository>().To<FoodDonationRepository>();
            kernel.Bind<IFoodDonationCommentRepository>().To<FoodDonationCommentRepository>();

            kernel.Bind<IFoodRequestRepository>().To<FoodRequestRepository>();
            kernel.Bind<IFoodRequestCommentRepository>().To<FoodRequestCommentRepository>();
        }        
    }
}
