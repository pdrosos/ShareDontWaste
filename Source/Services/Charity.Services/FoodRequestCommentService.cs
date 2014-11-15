namespace Charity.Services
{
    using System;
    using System.Linq;
    using Charity.Data.Common.Repositories;
    using Charity.Data.Models;
    using Charity.Services.Common;

    public class FoodRequestCommentService : IFoodRequestCommentService
    {
        private readonly IFoodRequestCommentRepository foodRequestCommentRepository;

        public FoodRequestCommentService(IFoodRequestCommentRepository foodRequestCommentRepository)
        {
            this.foodRequestCommentRepository = foodRequestCommentRepository;
        }
        
        public void Update(FoodRequestComment comment)
        {
            this.foodRequestCommentRepository.Update(comment);
            this.foodRequestCommentRepository.SaveChanges();
        }

        public void Add(FoodRequestComment comment)
        {
            this.foodRequestCommentRepository.Add(comment);
            this.foodRequestCommentRepository.SaveChanges();
        }

        public IQueryable<FoodRequestComment> All(int foodRequestId)
        {
            return this.foodRequestCommentRepository.All()
                .Where(c => c.FoodRequestId == foodRequestId)
                .OrderBy(c => c.CreatedOn);
        }
    }
}
