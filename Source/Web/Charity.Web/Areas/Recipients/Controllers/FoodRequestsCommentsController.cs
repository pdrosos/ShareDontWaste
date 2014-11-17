namespace Charity.Web.Areas.Recipients.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using Charity.Common;
    using Charity.Data.Models;
    using Charity.Services.Common;
    using Charity.Web.Areas.Recipients.Models;
    using Charity.Web.Infrastructure.Identity;

    [Authorize]
    public class FoodRequestsCommentsController : Controller
    {
        private readonly IFoodRequestService foodRequestService;
        private readonly IFoodRequestCommentService foodRequestCommentService;
        private readonly ICurrentUser currentUserProvider;

        public FoodRequestsCommentsController(
            IFoodRequestService foodRequestService,
            IFoodRequestCommentService foodRequestCommentService,
            ICurrentUser currentUserProvider)
        {
            this.foodRequestService = foodRequestService;
            this.foodRequestCommentService = foodRequestCommentService;
            this.currentUserProvider = currentUserProvider;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FoodRequestCommentCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var foodRequestComment = Mapper.Map<FoodRequestCommentCreateModel, FoodRequestComment>(model);

                ApplicationUser user = this.currentUserProvider.Get();

                foodRequestComment.ApplicationUserId = user.Id;

                var foodRequest = this.foodRequestService.GetById(model.FoodRequestId);
                if (foodRequest == null)
                {
                    throw new HttpException(404, "Food request not found");
                }

                if (foodRequest.Recipient.ApplicationUserId == user.Id)
                {
                    foodRequestComment.IsReadFromRecipient = true;
                }
                else if (foodRequest.FoodDonation.Donor.ApplicationUserId == user.Id)
                {
                    foodRequestComment.IsReadFromDonor = true;
                }
                else if (User.IsInRole(GlobalConstants.AdministratorRoleName) == false)
                {
                    throw new HttpException(400, "Invalid comment");
                }

                this.foodRequestCommentService.Add(foodRequestComment);

                var viewModel = Mapper.Map<FoodRequestCommentViewModel>(foodRequestComment);

                return PartialView("~/Areas/Recipients/Views/FoodRequests/_FoodRequestComment.cshtml", viewModel);
            }

            throw new HttpException(400, "Invalid comment");
        }
    }
}
