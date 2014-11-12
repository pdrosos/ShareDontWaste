namespace Charity.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Charity.Common;
    using Charity.Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;

            // TODO: Remove in production
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            this.SeedRoles(context);
            this.SeedAdminUser(context);
            this.SeedCities(context);
            this.SeedRecipientTypes(context);
            this.SeedFoodCategories(context);
        }

        private void SeedRoles(ApplicationDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }

            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.AdministratorRoleName));
            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.DonorRoleName));
            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.RecipientRoleName));

            context.SaveChanges();
        }

        private void SeedAdminUser(ApplicationDbContext context)
        {
            if (context.Administrators.Any())
            {
                return;
            }

            var userManager = this.CreateUserManager(context);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            
            var administratorProfile = new Administrator();
            administratorProfile.FirstName = "Admin";
            administratorProfile.LastName = "Admin";

            // Create Admin Role if it does not exist
            if (! roleManager.RoleExists(GlobalConstants.AdministratorRoleName))
            {
                roleManager.Create(new IdentityRole(GlobalConstants.AdministratorRoleName));
            }

            // Create Admin User with password
            var administratorUser = new ApplicationUser();
            administratorUser.UserName = "admin";
            administratorUser.Email = "admin@admin.com";
            administratorUser.CreatedOn = DateTime.Now;
            string password = "111";

            var result = userManager.Create(administratorUser, password);

            // Add Admin User to Admin Role
            if (result.Succeeded)
            {
                userManager.AddToRole(administratorUser.Id, GlobalConstants.AdministratorRoleName);
            }

            // Add Admin User to Admin Profile
            administratorProfile.ApplicationUser = administratorUser;
            context.Administrators.Add(administratorProfile);

            context.SaveChanges();
        }

        private UserManager<ApplicationUser> CreateUserManager(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // Configure user manager
            // Configure validation logic for usernames
            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 3,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            return userManager;
        }

        private void SeedCities(ApplicationDbContext context)
        {
            if (context.Cities.Any())
            {
                return;
            }

            var city = new City();
            city.Name = "Sofia";
            city.CreatedOn = DateTime.Now;
            context.Cities.Add(city);

            city = new City();
            city.Name = "Plovdiv";
            city.CreatedOn = DateTime.Now;
            context.Cities.Add(city);

            city = new City();
            city.Name = "Varna";
            city.CreatedOn = DateTime.Now;
            context.Cities.Add(city);

            city = new City();
            city.Name = "Burgas";
            city.CreatedOn = DateTime.Now;
            context.Cities.Add(city);

            context.SaveChanges();
        }

        private void SeedRecipientTypes(ApplicationDbContext context)
        {
            if (context.RecipientTypes.Any())
            {
                return;
            }

            var type = new RecipientType();
            type.Name = "Homeless Centre";
            type.CreatedOn = DateTime.Now;
            context.RecipientTypes.Add(type);

            type = new RecipientType();
            type.Name = "Crisis Accommodation";
            type.CreatedOn = DateTime.Now;
            context.RecipientTypes.Add(type);

            type = new RecipientType();
            type.Name = "School";
            type.CreatedOn = DateTime.Now;
            context.RecipientTypes.Add(type);

            type = new RecipientType();
            type.Name = "Animal shelter";
            type.CreatedOn = DateTime.Now;
            context.RecipientTypes.Add(type);

            type = new RecipientType();
            type.Name = "Other";
            type.CreatedOn = DateTime.Now;
            context.RecipientTypes.Add(type);

            context.SaveChanges();
        }

        private void SeedFoodCategories(ApplicationDbContext context)
        {
            if (context.FoodCategories.Any())
            {
                return;
            }

            var type = new FoodCategory();
            type.Name = "Meat";
            type.CreatedOn = DateTime.Now;
            context.FoodCategories.Add(type);

            type = new FoodCategory();
            type.Name = "Seafood";
            type.CreatedOn = DateTime.Now;
            context.FoodCategories.Add(type);

            type = new FoodCategory();
            type.Name = "Egg Products";
            type.CreatedOn = DateTime.Now;
            context.FoodCategories.Add(type);

            type = new FoodCategory();
            type.Name = "Dairy Products";
            type.CreatedOn = DateTime.Now;
            context.FoodCategories.Add(type);

            type = new FoodCategory();
            type.Name = "Fresh fruits and vegetables";
            type.CreatedOn = DateTime.Now;
            context.FoodCategories.Add(type);

            type = new FoodCategory();
            type.Name = "Nuts, Grains and Beans";
            type.CreatedOn = DateTime.Now;
            context.FoodCategories.Add(type);

            type = new FoodCategory();
            type.Name = "Ready meals";
            type.CreatedOn = DateTime.Now;
            context.FoodCategories.Add(type);

            type = new FoodCategory();
            type.Name = "Baby food";
            type.CreatedOn = DateTime.Now;
            context.FoodCategories.Add(type);

            type = new FoodCategory();
            type.Name = "Pet food";
            type.CreatedOn = DateTime.Now;
            context.FoodCategories.Add(type);

            type = new FoodCategory();
            type.Name = "Other";
            type.CreatedOn = DateTime.Now;
            context.FoodCategories.Add(type);

            context.SaveChanges();
        }
    }
}
