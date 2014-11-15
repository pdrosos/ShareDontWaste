namespace Charity.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Charity.Common;
    using Charity.Data.Extensions;
    using Charity.Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        private readonly List<string> organizationNames = new List<string>()
        {
            "Acme",
            "Universal Exports",
            "Smith and Co.",
            "Allied Biscuit",
            "Galaxy",
            "Globex",
            "ZiffCorp",
            "Gringotts",
            "Water and Power",
            "Bluth",
            "Praxis",
            "Luthor",
            "Tessier-Ashpool",
            "Three Waters",
            "Sto Plains",
            "Mooby Foods",
            "Strickland",
            "AnimalHope",
            "Green Planet",
            "North Central",
        };

        private readonly List<string> contactNames = new List<string>()
        {
            "Teddy Ferrara",
            "Dyan Fisher",
            "Anne Smith",
            "Maria Finnegan",
            "Ronnie Foltz",
            "Eleanor Fowler",
            "William Heller",
            "Bobbi Canfield",
            "Christina Buxton",
            "Alexander Byrnes",
            "Simon Cambell",
            "Peter Callaghan",
            "Ashley Hong",
            "Hayden Jacques",
            "Ida Jacobson",
            "Jamie Miller",
            "Jason Peterson",
            "Michael Kaiser",
            "Ivy Kearney",
            "Sammy Keen",
        };

        private readonly Random randomGenerator = new Random();

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

            var userManager = this.CreateUserManager(context);            
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            context.Configuration.AutoDetectChangesEnabled = false;

            this.SeedRoles(context);
            this.SeedAdminUser(context, userManager, roleManager);

            var cities = this.SeedCities(context);
            var recipientTypes = this.SeedRecipientTypes(context);
            var foodCategories = this.SeedFoodCategories(context);

            var donors = this.SeedDonors(context, userManager, roleManager, cities);
            var recipients = this.SeedRecipients(
                context, 
                userManager, 
                roleManager,
                recipientTypes,
                cities,
                foodCategories);

            var foodDonations = this.SeedFoodDonations(context, foodCategories, donors);
            this.SeedFoodRequests(context, recipients, foodDonations);

            context.Configuration.AutoDetectChangesEnabled = true;
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

        private void SeedAdminUser(
            ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            if (context.Administrators.Any())
            {
                return;
            }
            
            var administratorProfile = new Administrator();
            administratorProfile.FirstName = "Admin";
            administratorProfile.LastName = "Admin";
            administratorProfile.CreatedOn = DateTime.Now;

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

        private List<Donor> SeedDonors(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            List<City> cities)
        {
            var donors = new List<Donor>();

            if (context.Donors.Any())
            {
                return donors;
            }

            for (int i = 1; i <= 20; i++)
            {
                var donorProfile = new Donor();
                donorProfile.OrganizationName = this.organizationNames[i - 1];
                donorProfile.ContactName = this.contactNames[20 - i];

                var cityIndex = this.randomGenerator.Next(0, cities.Count);
                donorProfile.City = cities[cityIndex];

                donorProfile.CreatedOn = DateTime.Now;

                // Create Donor Role if it does not exist
                if (!roleManager.RoleExists(GlobalConstants.DonorRoleName))
                {
                    roleManager.Create(new IdentityRole(GlobalConstants.DonorRoleName));
                }

                // Create Donor User with password
                var donorUser = new ApplicationUser();
                donorUser.UserName = "donor" + i;
                donorUser.Email = "d" + i + "@d.com";
                donorUser.CreatedOn = DateTime.Now;
                string password = "111";

                var result = userManager.Create(donorUser, password);

                // Add Donor User to Donor Role
                if (result.Succeeded)
                {
                    userManager.AddToRole(donorUser.Id, GlobalConstants.DonorRoleName);
                }

                // Add Donor User to Donor Profile
                donorProfile.ApplicationUser = donorUser;
                context.Donors.Add(donorProfile);

                donors.Add(donorProfile);
            }

            context.SaveChanges();

            return donors;
        }

        private List<Recipient> SeedRecipients(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            List<RecipientType> recipientTypes,
            List<City> cities,
            List<FoodCategory> foodCategories)
        {
            var recipients = new List<Recipient>();

            if (context.Recipients.Any())
            {
                return recipients;
            }

            for (int i = 1; i <= 20; i++)
            {
                var recipientProfile = new Recipient();
                recipientProfile.OrganizationName = this.organizationNames[20 - i];
                recipientProfile.ContactName = this.contactNames[20 - i];

                var recipientTypeIndex = this.randomGenerator.Next(0, recipientTypes.Count);
                recipientProfile.RecipientType = recipientTypes[recipientTypeIndex];

                var cityIndex = this.randomGenerator.Next(0, cities.Count);
                recipientProfile.City = cities[cityIndex];
                
                foodCategories.Shuffle();
                recipientProfile.FoodCategories = foodCategories.Take(5).ToList();
                
                recipientProfile.CreatedOn = DateTime.Now;

                // Create Recipient Role if it does not exist
                if (!roleManager.RoleExists(GlobalConstants.RecipientRoleName))
                {
                    roleManager.Create(new IdentityRole(GlobalConstants.RecipientRoleName));
                }

                // Create Recipient User with password
                var donorUser = new ApplicationUser();
                donorUser.UserName = "recipient" + i;
                donorUser.Email = "r" + i + "@r.com";
                donorUser.CreatedOn = DateTime.Now;
                string password = "111";

                var result = userManager.Create(donorUser, password);

                // Add Recipient User to Recipient Role
                if (result.Succeeded)
                {
                    userManager.AddToRole(donorUser.Id, GlobalConstants.RecipientRoleName);
                }

                // Add Recipient User to Recipient Profile
                recipientProfile.ApplicationUser = donorUser;
                context.Recipients.Add(recipientProfile);

                recipients.Add(recipientProfile);
            }

            context.SaveChanges();

            return recipients;
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

        private List<City> SeedCities(ApplicationDbContext context)
        {
            var cities = new List<City>();

            if (context.Cities.Any())
            {
                return cities;
            }

            var city = new City();
            city.Name = "Sofia";
            city.CreatedOn = DateTime.Now;
            cities.Add(city);
            context.Cities.Add(city);

            city = new City();
            city.Name = "Plovdiv";
            city.CreatedOn = DateTime.Now;
            cities.Add(city);
            context.Cities.Add(city);

            city = new City();
            city.Name = "Varna";
            city.CreatedOn = DateTime.Now;
            cities.Add(city);
            context.Cities.Add(city);

            city = new City();
            city.Name = "Burgas";
            city.CreatedOn = DateTime.Now;
            cities.Add(city);
            context.Cities.Add(city);

            context.SaveChanges();

            return cities;
        }

        private List<RecipientType> SeedRecipientTypes(ApplicationDbContext context)
        {
            var recipientTypes = new List<RecipientType>();

            if (context.RecipientTypes.Any())
            {
                return recipientTypes;
            }

            var type = new RecipientType();
            type.Name = "Homeless Centre";
            type.CreatedOn = DateTime.Now;
            recipientTypes.Add(type);
            context.RecipientTypes.Add(type);

            type = new RecipientType();
            type.Name = "Crisis Accommodation";
            type.CreatedOn = DateTime.Now;
            recipientTypes.Add(type);
            context.RecipientTypes.Add(type);

            type = new RecipientType();
            type.Name = "School";
            type.CreatedOn = DateTime.Now;
            recipientTypes.Add(type);
            context.RecipientTypes.Add(type);

            type = new RecipientType();
            type.Name = "Animal shelter";
            type.CreatedOn = DateTime.Now;
            recipientTypes.Add(type);
            context.RecipientTypes.Add(type);

            type = new RecipientType();
            type.Name = "Other";
            type.CreatedOn = DateTime.Now;
            recipientTypes.Add(type);
            context.RecipientTypes.Add(type);

            context.SaveChanges();

            return recipientTypes;
        }

        private List<FoodCategory> SeedFoodCategories(ApplicationDbContext context)
        {
            var foodCategories = new List<FoodCategory>();

            if (context.FoodCategories.Any())
            {
                return foodCategories;
            }

            var category = new FoodCategory();
            category.Name = "Meat";
            category.CreatedOn = DateTime.Now;
            foodCategories.Add(category);
            context.FoodCategories.Add(category);

            category = new FoodCategory();
            category.Name = "Seafood";
            category.CreatedOn = DateTime.Now;
            foodCategories.Add(category);
            context.FoodCategories.Add(category);

            category = new FoodCategory();
            category.Name = "Egg Products";
            category.CreatedOn = DateTime.Now;
            foodCategories.Add(category);
            context.FoodCategories.Add(category);

            category = new FoodCategory();
            category.Name = "Dairy Products";
            category.CreatedOn = DateTime.Now;
            foodCategories.Add(category);
            context.FoodCategories.Add(category);

            category = new FoodCategory();
            category.Name = "Fresh fruits and vegetables";
            category.CreatedOn = DateTime.Now;
            foodCategories.Add(category);
            context.FoodCategories.Add(category);

            category = new FoodCategory();
            category.Name = "Nuts, Grains and Beans";
            category.CreatedOn = DateTime.Now;
            foodCategories.Add(category);
            context.FoodCategories.Add(category);

            category = new FoodCategory();
            category.Name = "Ready meals";
            category.CreatedOn = DateTime.Now;
            foodCategories.Add(category);
            context.FoodCategories.Add(category);

            category = new FoodCategory();
            category.Name = "Baby food";
            category.CreatedOn = DateTime.Now;
            foodCategories.Add(category);
            context.FoodCategories.Add(category);

            category = new FoodCategory();
            category.Name = "Pet food";
            category.CreatedOn = DateTime.Now;
            foodCategories.Add(category);
            context.FoodCategories.Add(category);

            category = new FoodCategory();
            category.Name = "Other";
            category.CreatedOn = DateTime.Now;
            foodCategories.Add(category);
            context.FoodCategories.Add(category);

            context.SaveChanges();

            return foodCategories;
        }

        private List<FoodDonation> SeedFoodDonations(ApplicationDbContext context, List<FoodCategory> foodCategories, List<Donor> donors)
        {
            var foodDonations = new List<FoodDonation>();

            if (context.FoodDonations.Any())
            {
                return foodDonations;
            }

            for (int i = 0; i < 20; i++)
            {
                for (int j = 1; j <= 20; j++)
                {
                    var foodDonation = new FoodDonation();
                    var categoryIndex = this.randomGenerator.Next(0, foodCategories.Count);
                    var foodCategory = foodCategories[categoryIndex];

                    foodDonation.Donor = donors[i];
                    foodDonation.FoodCategory = foodCategory;
                    foodDonation.Name = foodCategory.Name;
                    foodDonation.Quantity = j.ToString() + (j == 1 ? " item" : " items");
                    foodDonation.Description = foodCategory.Name;

                    foodDonation.ExpirationDate = DateTime.Now.AddDays(j + 10);
                    foodDonation.AvailableFrom = DateTime.Now;
                    foodDonation.AvailableTo = foodDonation.ExpirationDate.AddDays(-3);

                    foodDonation.CreatedOn = DateTime.Now;

                    context.FoodDonations.Add(foodDonation);
                    foodDonations.Add(foodDonation);
                }
            }

            context.SaveChanges();

            return foodDonations;
        }

        private void SeedFoodRequests(ApplicationDbContext context, List<Recipient> recipients, List<FoodDonation> foodDonations)
        {
            if (context.FoodRequests.Any())
            {
                return;
            }

            for (int i = 0; i < 20; i++)
            {
                for (int j = 1; j <= 20; j++)
                {
                    var foodRequest = new FoodRequest();
                    var foodDonationIndex = this.randomGenerator.Next(0, foodDonations.Count);
                    var foodDonation = foodDonations[foodDonationIndex];

                    foodRequest.Recipient = recipients[i];
                    foodRequest.FoodDonation = foodDonation;
                    foodRequest.Quantity = j.ToString() + (j == 1 ? " item" : " items");
                    foodRequest.Description = foodDonation.Name;

                    foodRequest.NeedFrom = DateTime.Now;
                    foodRequest.NeedTo = foodDonation.AvailableTo;

                    foodRequest.CreatedOn = DateTime.Now;

                    context.FoodRequests.Add(foodRequest);
                }
            }

            context.SaveChanges();
        }
    }
}
