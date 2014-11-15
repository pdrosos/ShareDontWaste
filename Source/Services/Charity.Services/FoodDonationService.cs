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

        public void Delete(FoodDonation foodDonation)
        {
            this.foodDonationRepository.Delete(foodDonation);
            this.foodDonationRepository.SaveChanges();
        }
    }
}
