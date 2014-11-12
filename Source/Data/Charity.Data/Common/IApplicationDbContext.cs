namespace Charity.Data.Common
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Charity.Data.Models;

    public interface IApplicationDbContext : IDisposable
    {
        IDbSet<ApplicationUser> Users { get; set; }

        IDbSet<City> Cities { get; set; }

        IDbSet<Administrator> Administrators { get; set; }

        IDbSet<RecipientType> RecipientTypes { get; set; }

        IDbSet<Recipient> Recipients { get; set; }

        IDbSet<FoodCategory> FoodCategories { get; set; }

        IDbSet<FoodDonation> FoodDonations { get; set; }

        int SaveChanges();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
