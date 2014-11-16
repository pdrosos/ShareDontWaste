namespace Charity.Services
{
    using System;
    using System.Linq;
    using Charity.Data.Common.Repositories;
    using Charity.Data.Models;
    using Charity.Services.Common;

    public class FoodRequestService : IFoodRequestService
    {
        private readonly IFoodRequestRepository foodRequestRepository;

        public FoodRequestService(IFoodRequestRepository foodRequestRepository)
        {
            this.foodRequestRepository = foodRequestRepository;
        }

        public FoodRequest GetById(int id)
        {
            return this.foodRequestRepository.GetById(id);
        }

        public FoodRequest GetByDonationIdAndRecipientId(int donationId, Guid recipientId)
        {
            return this.foodRequestRepository.All()
                .FirstOrDefault(r => r.FoodDonationId == donationId && r.RecipientId == recipientId);
        }

        public void Update(FoodRequest foodRequest)
        {
            this.foodRequestRepository.Update(foodRequest);
            this.foodRequestRepository.SaveChanges();
        }

        public void Add(FoodRequest foodRequest)
        {
            this.foodRequestRepository.Add(foodRequest);
            this.foodRequestRepository.SaveChanges();
        }

        public IQueryable<FoodRequest> All()
        {
            return this.foodRequestRepository.All();
        }

        public IQueryable<FoodRequest> List()
        {
            var query = this.All()
                .Where(d => d.IsCompleted == false)
                .OrderByDescending(d => d.CreatedOn);

            return query;
        }

        public IQueryable<FoodRequest> ListByDonation(int donationId)
        {
            var query = this.All()
                .Where(d => d.FoodDonationId == donationId)
                .OrderByDescending(d => d.CreatedOn);

            return query;
        }

        public void Delete(FoodRequest foodRequest)
        {
            this.foodRequestRepository.Delete(foodRequest);
            this.foodRequestRepository.SaveChanges();
        }
    }
}
