﻿namespace Charity.Services
{
    using System;
    using System.Linq;
    using Charity.Data.Common.Repositories;
    using Charity.Data.Models;

    public class FoodDonationService
    {
        private readonly IFoodDonationRepository foodDonationRepository;

        public FoodDonationService(IFoodDonationRepository foodDonationRepository)
        {
            this.foodDonationRepository = foodDonationRepository;
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
    }
}