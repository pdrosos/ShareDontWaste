namespace Charity.Services.Common
{
    using System;
    using System.Linq;
    using Charity.Data.Models;

    public interface IFoodRequestService
    {
        FoodRequest GetById(int id);

        void Update(FoodRequest foodRequest);

        void Add(FoodRequest foodRequest);

        IQueryable<FoodRequest> All();

        IQueryable<FoodRequest> List();

        IQueryable<FoodRequest> ListByDonation(int donationId);

        void Delete(FoodRequest foodDonation);
    }
}
