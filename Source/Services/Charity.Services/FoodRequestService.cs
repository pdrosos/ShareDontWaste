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
    }
}
