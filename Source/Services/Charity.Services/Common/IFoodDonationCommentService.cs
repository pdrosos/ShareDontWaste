namespace Charity.Services.Common
{
    using System;
    using System.Linq;
    using Charity.Data.Models;

    public interface IFoodDonationCommentService
    {
        void Update(FoodDonationComment comment);

        void Add(FoodDonationComment comment);

        IQueryable<FoodDonationComment> All(int foodDonationId);
    }
}
