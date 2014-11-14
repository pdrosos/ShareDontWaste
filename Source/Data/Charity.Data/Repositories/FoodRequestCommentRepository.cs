namespace Charity.Data.Repositories
{
    using System;
    using System.Linq;
    using Charity.Data.Common;
    using Charity.Data.Common.Repositories;
    using Charity.Data.Models;

    public class FoodRequestCommentRepository : DeletableEntityRepository<FoodRequestComment>, IFoodRequestCommentRepository
    {
        public FoodRequestCommentRepository(IApplicationDbContext context)
            : base(context)
        {
        }
    }
}