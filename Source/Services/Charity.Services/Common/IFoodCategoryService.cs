namespace Charity.Services.Common
{
    using System;
    using System.Linq;
    using Charity.Data.Models;

    public interface IFoodCategoryService
    {
        IQueryable<FoodCategory> GetAll();

        FoodCategory GetById(int id);
    }
}
