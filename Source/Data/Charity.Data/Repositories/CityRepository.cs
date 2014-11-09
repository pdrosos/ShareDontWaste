namespace Charity.Data.Repositories
{
    using System;
    using System.Linq;
    using Charity.Data.Models;

    public class CityRepository : DeletableEntityRepository<City>
    {
        public CityRepository(IApplicationDbContext context)
            : base(context)
        {
        }
    }
}