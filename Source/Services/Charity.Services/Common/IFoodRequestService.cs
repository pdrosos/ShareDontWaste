namespace Charity.Services.Common
{
    using System;
    using System.Linq;
    using Charity.Data.Models;

    public interface IFoodRequestService
    {
        FoodRequest GetById(int id);

        FoodRequest GetByDonationIdAndRecipientId(int donationId, Guid recipientId);

        void Update(FoodRequest foodRequest);

        void Add(FoodRequest foodRequest);

        IQueryable<FoodRequest> All();

        IQueryable<FoodRequest> List();

        IQueryable<FoodRequest> ListByDonation(int donationId);

        IQueryable<FoodRequest> ListByDonor(Guid donorId);

        void Delete(FoodRequest foodDonation);
    }
}
