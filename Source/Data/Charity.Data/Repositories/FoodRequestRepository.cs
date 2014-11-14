namespace Charity.Data.Repositories
{
    using System;
    using System.Linq;
    using Charity.Data.Common;
    using Charity.Data.Common.Repositories;
    using Charity.Data.Models;

    public class FoodRequestRepository : DeletableEntityRepository<FoodRequest>, IFoodRequestRepository
    {
        public FoodRequestRepository(IApplicationDbContext context)
            : base(context)
        {
        }
    }
}