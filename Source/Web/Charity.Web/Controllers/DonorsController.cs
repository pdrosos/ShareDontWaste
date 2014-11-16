namespace Charity.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Charity.Data.Models;
    using Charity.Services.Common;
    using Charity.Web.Models.Donors;
    using PagedList;

    public class DonorsController : Controller
    {
        private readonly IDonorProfileService donorProfileService;

        public DonorsController(
            IDonorProfileService donorProfileService)
        {
            this.donorProfileService = donorProfileService;
        }
        
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageIndex = (page ?? 1);

            var allDonors = this.donorProfileService.List().Project().To<DonorViewModel>();

            var donorsPagedList = allDonors.ToPagedList(pageIndex, pageSize);

            var viewModel = new DonorListViewModel();
            viewModel.Donors = donorsPagedList;

            return View(viewModel);
        }

        public ActionResult Details(string userName)
        {
            if (userName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var donor = this.donorProfileService.GetByUserName(userName);

            if (donor == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<Donor, DonorDetailsViewModel>(donor);

            return View(model);
        }
    }
}