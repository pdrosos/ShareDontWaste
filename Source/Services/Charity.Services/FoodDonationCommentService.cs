namespace Charity.Services
{
    using System;
    using System.Linq;
    using Charity.Data.Common.Repositories;
    using Charity.Data.Models;
    using Charity.Services.Common;

    public class FoodDonationCommentService : IFoodDonationCommentService
    {
        private readonly IFoodDonationCommentRepository foodDonationCommentRepository;

        public FoodDonationCommentService(IFoodDonationCommentRepository foodDonationCommentRepository)
        {
            this.foodDonationCommentRepository = foodDonationCommentRepository;
        }
        
        public void Update(FoodDonationComment comment)
        {
            this.foodDonationCommentRepository.Update(comment);
            this.foodDonationCommentRepository.SaveChanges();
        }

        public void Add(FoodDonationComment comment)
        {
            this.foodDonationCommentRepository.Add(comment);
            this.foodDonationCommentRepository.SaveChanges();
        }

        public IQueryable<FoodDonationComment> All(int foodDonationId)
        {
            return this.foodDonationCommentRepository.All()
                .Where(c => c.FoodDonationId == foodDonationId)
                .OrderBy(c => c.CreatedOn);
        }
    }
}
