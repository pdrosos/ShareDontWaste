namespace Charity.Data.Repositories
{
    using System;
    using System.Linq;
    using Charity.Data.Common;
    using Charity.Data.Common.Repositories;
    using Charity.Data.Models;

    public class FoodDonationRepository : DeletableEntityRepository<FoodDonation>, IFoodDonationRepository
    {
        public FoodDonationRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}