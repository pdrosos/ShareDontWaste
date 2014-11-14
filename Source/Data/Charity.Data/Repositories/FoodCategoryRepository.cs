namespace Charity.Data.Repositories
{
    using System;
    using System.Linq;
    using Charity.Data.Common;
    using Charity.Data.Common.Repositories;
    using Charity.Data.Models;

    public class FoodCategoryRepository : DeletableEntityRepository<FoodCategory>, IFoodCategoryRepository
    {
        public FoodCategoryRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}