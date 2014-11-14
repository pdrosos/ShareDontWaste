namespace Charity.Data.Repositories
{
    using System;
    using System.Linq;
    using Charity.Data.Common;
    using Charity.Data.Common.Repositories;
    using Charity.Data.Models;

    public class CityRepository : DeletableEntityRepository<City>, ICityRepository
    {
        public CityRepository(IApplicationDbContext context)
            : base(context)
        {
        }
    }
}