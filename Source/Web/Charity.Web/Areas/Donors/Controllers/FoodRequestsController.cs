namespace Charity.Web.Areas.Donors.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Charity.Common;
    using Charity.Data.Models;
    using Charity.Services.Common;
    using Charity.Web.Areas.Donors.Models;
    using Charity.Web.Infrastructure.Identity;
    using PagedList;

    [Authorize(Roles = GlobalConstants.DonorRoleName)]
    public class FoodRequestsController : Controller
    {
        private readonly IFoodRequestService foodRequestService;
        private readonly IDonorProfileService donorProfileService;
        private readonly ICurrentUser currentUserProvider;
        private readonly IFoodRequestCommentService foodRequestCommentService;

        public FoodRequestsController(
            IFoodRequestService foodRequestService,
            IDonorProfileService donorProfileService,
            ICurrentUser currentUserProvider,
            IFoodRequestCommentService foodRequestCommentService)
        {
            this.foodRequestService = foodRequestService;
            this.donorProfileService = donorProfileService;
            this.currentUserProvider = currentUserProvider;
            this.foodRequestCommentService = foodRequestCommentService;
        }

        public ActionResult MyRequests(int? page)
        {
            int pageSize = 10;
            int pageIndex = (page ?? 1);

            ApplicationUser user = this.currentUserProvider.Get();
            var donor = this.donorProfileService.GetByApplicationUserId(user.Id);

            var foodRequests = this.foodRequestService.ListByDonor(donor.Id)
                .Project().To<FoodRequestListViewModel>();

            var foodRequestsPagedList = foodRequests.ToPagedList(pageIndex, pageSize);

            return View(foodRequestsPagedList);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var foodRequest = this.foodRequestService.GetById((int)id);

            if (foodRequest == null)
            {
                return HttpNotFound();
            }

            ApplicationUser user = this.currentUserProvider.Get();
            var donor = this.donorProfileService.GetByApplicationUserId(user.Id);

            if (foodRequest.FoodDonation.DonorId != donor.Id)
            {
                throw new HttpException(400, "The request is not for your donation");
            }

            var model = Mapper.Map<FoodRequest, FoodRequestViewModel>(foodRequest);

            // Mark all comments as read
            this.foodRequestCommentService.MarkCommentsAsReadFromDonor(foodRequest.Id);

            return View(model);
        }
    }
}
