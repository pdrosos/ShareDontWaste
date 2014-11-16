namespace Charity.Services
{
    using System;
    using System.Linq;
    using Charity.Data.Common.Repositories;
    using Charity.Data.Models;
    using Charity.Services.Common;

    public class FoodDonationService : IFoodDonationService
    {
        private readonly IFoodDonationRepository foodDonationRepository;

        public FoodDonationService(IFoodDonationRepository foodDonationRepository)
        {
            this.foodDonationRepository = foodDonationRepository;
        }

        public FoodDonation GetById(int id)
        {
            return this.foodDonationRepository.GetById(id);
        }
        
        public void Update(FoodDonation foodDonation)
        {
            this.foodDonationRepository.Update(foodDonation);
            this.foodDonationRepository.SaveChanges();
        }

        public void Add(FoodDonation foodDonation)
        {
            this.foodDonationRepository.Add(foodDonation);
            this.foodDonationRepository.SaveChanges();
        }

        public IQueryable<FoodDonation> All()
        {
            return this.foodDonationRepository.All();
        }

        public IQueryable<FoodDonation> GetLatestDonations(int latestDonationsCount)
        {
            var query = this.All()
                .OrderByDescending(d => d.Id)
                .Take(latestDonationsCount);

            return query;
        }

        public IQueryable<FoodDonation> List()
        {
            var query = this.All()
                .Where(d => d.IsCompleted == false)
                .OrderByDescending(d => d.CreatedOn);

            return query;
        }

        public IQueryable<FoodDonation> ListByCategory(int categoryId)
        {
            var query = this.All()
                .Where(d => d.IsCompleted == false && d.FoodCategoryId == categoryId)
                .OrderByDescending(d => d.CreatedOn);                

            return query;
        }

        public void Delete(FoodDonation foodDonation)
        {
            this.foodDonationRepository.Delete(foodDonation);
            this.foodDonationRepository.SaveChanges();
        }
    }
}
