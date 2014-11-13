namespace Charity.Data.Repositories
{
    using System;
    using System.Linq;
    using Charity.Data.Common;
    using Charity.Data.Common.Repositories;
    using Charity.Data.Models;

    public class FoodDonationCommentRepository : DeletableEntityRepository<FoodDonationComment>, IFoodDonationCommentRepository
    {
        public FoodDonationCommentRepository(IApplicationDbContext context)
            : base(context)
        {
        }
    }
}