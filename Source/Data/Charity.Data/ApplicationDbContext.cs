namespace Charity.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Charity.Data.Common;
    using Charity.Data.Migrations;
    using Charity.Data.Models;
    using Charity.Data.Models.Common;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Validation;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public IDbSet<City> Cities { get; set; }

        public IDbSet<Administrator> Administrators { get; set; }

        public IDbSet<Donor> Donors { get; set; }

        public IDbSet<RecipientType> RecipientTypes { get; set; }

        public IDbSet<Recipient> Recipients { get; set; }

        public IDbSet<FoodCategory> FoodCategories { get; set; }

        public IDbSet<FoodDonation> FoodDonations { get; set; }

        public IDbSet<FoodDonationComment> FoodDonationComments { get; set; }

        public IDbSet<FoodRequest> FoodRequests { get; set; }

        public IDbSet<FoodRequestComment> FoodRequestComments { get; set; }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            //this.ApplyDeletableEntityRules();

            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        private void ApplyDeletableEntityRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (
                var entry in
                    this.ChangeTracker.Entries()
                        .Where(e => e.Entity is ISoftDeletable && (e.State == EntityState.Deleted)))
            {
                var entity = (ISoftDeletable)entry.Entity;

                entity.DeletedOn = DateTime.Now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}
