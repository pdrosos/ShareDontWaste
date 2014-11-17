namespace Charity.Services
{
    using System;
    using System.Linq;
    using Charity.Data.Common.Repositories;
    using Charity.Data.Models;
    using Charity.Services.Common;

    public class FoodCategoryService : IFoodCategoryService
    {
        private readonly IFoodCategoryRepository foodCategoryRepository;

        public FoodCategoryService(IFoodCategoryRepository foodCategoryRepository)
        {
            this.foodCategoryRepository = foodCategoryRepository;
        }

        public IQueryable<FoodCategory> GetAll()
        {
            return this.foodCategoryRepository.All().OrderBy(c => c.Id);
        }

        public FoodCategory GetById(int id)
        {
            return this.foodCategoryRepository.GetById(id);
        }
    }
}
