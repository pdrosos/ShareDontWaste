namespace Charity.Services.Common
{
    using System;
    using System.Linq;
    using Charity.Data.Models;

    public interface IFoodDonationService
    {
        FoodDonation GetById(int id);

        void Update(FoodDonation foodDonation);

        void Add(FoodDonation foodDonation);

        IQueryable<FoodDonation> All();

        void Delete(FoodDonation foodDonation);
    }
}
