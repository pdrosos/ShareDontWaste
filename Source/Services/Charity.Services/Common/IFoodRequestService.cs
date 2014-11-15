namespace Charity.Services.Common
{
    using System;
    using System.Linq;
    using Charity.Data.Models;

    public interface IFoodRequestService
    {
        void Update(FoodRequest foodRequest);

        void Add(FoodRequest foodRequest);

        IQueryable<FoodRequest> All();
    }
}
