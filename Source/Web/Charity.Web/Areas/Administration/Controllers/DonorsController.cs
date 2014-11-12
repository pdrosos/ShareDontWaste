namespace Charity.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Charity.Common;
    using Charity.Data.Models;
    using Charity.Services;
    using Charity.Web.Areas.Administration.Models;
    using PagedList;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class DonorsController : Controller
    {
        private readonly DonorProfileService donorProfileService;
        private readonly CityService cityService;

        public DonorsController(DonorProfileService donorProfileService, CityService cityService)
        {
            this.donorProfileService = donorProfileService;
            this.cityService = cityService;
        }


        public ActionResult Index(int? page)
        {
            var allDonors = this.donorProfileService.All().Project().To<DonorListViewModel>();

            int pageSize = 3;
            int pageIndex = (page ?? 1);

            //IEnumerable<DonorListViewModel> allDonorsEnumerable = allDonors.AsEnumerable();
            return View(allDonors.OrderBy(o => o.OrganizationName).ToPagedList(pageIndex, pageSize));
        }

        public ActionResult Details(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Donor donor = this.donorProfileService.GetByUserName(username);

            if (donor == null)
            {
                return HttpNotFound();
            }

            DonorDetailsViewModel model = Mapper.Map<Donor, DonorDetailsViewModel>(donor);
            model.AccountDetailsViewModel = Mapper.Map<ApplicationUser, AccountDetailsViewModel>(donor.ApplicationUser);

            return View(model);
        }

        public ActionResult Edit(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Donor donor = this.donorProfileService.GetByUserName(username);

            if (donor == null)
            {
                return HttpNotFound();
            }

            DonorDetailsEditModel model = Mapper.Map<Donor, DonorDetailsEditModel>(donor);
            model.AccountDetailsEditModel = Mapper.Map<ApplicationUser, AccountDetailsEditModel>(donor.ApplicationUser);
            model.Cities = this.GetCities();

            return View(model);
        }

        // POST: Donors/Profile/Edit/username
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DonorDetailsEditModel model)
        {
            //ApplicationUser user = currentUserProvider.Get();
            Donor donor = this.donorProfileService.GetById(model.Id);
            
            if (ModelState.IsValid)
            {
                Mapper.Map<DonorDetailsEditModel, Donor>(model, donor);
                Mapper.Map<AccountDetailsEditModel, ApplicationUser>(model.AccountDetailsEditModel, donor.ApplicationUser);
                
                this.donorProfileService.Update(donor);

                return RedirectToAction("Index", "Donors");
            }

            model.Cities = this.GetCities();

            return View(model);
        }

        [HttpGet]
        public ActionResult Search(string name)
        {
            var foundDonors = this.donorProfileService
                .SearchByOrganizationName(name)
                .Project()
                .To<DonorListViewModel>();

            return View(foundDonors.AsEnumerable());
        }

        private IEnumerable<SelectListItem> GetCities()
        {
            var cities = this.cityService.GetAll()
                             .Select(c => new SelectListItem
                                    {
                                        Value = c.Id.ToString(),
                                        Text = c.Name
                                    });

            return new SelectList(cities, "Value", "Text");
        }
    }
}
