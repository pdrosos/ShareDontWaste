namespace Charity.Services.Common
{
    using System;
    using System.Linq;
    using Charity.Data.Models;

    public interface IFoodRequestCommentService
    {
        void Update(FoodRequestComment comment);

        void Add(FoodRequestComment comment);

        IQueryable<FoodRequestComment> All(int foodRequestId);

        void MarkCommentsAsReadFromDonor(int foodRequestId);

        void MarkCommentsAsReadFromRecipient(int foodRequestId);
    }
}
