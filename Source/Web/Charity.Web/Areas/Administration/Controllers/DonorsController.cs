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

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.OrganizationNameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.ContactNameSortParam = sortOrder == "ContactName" ? "contactname_desc" : "ContactName";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var donors = this.donorProfileService.All();

            if (!String.IsNullOrEmpty(searchString))
            {
                donors = donors.Where(d => d.OrganizationName.Contains(searchString)
                                       || d.ContactName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    donors = donors.OrderByDescending(d => d.OrganizationName);
                    break;
                case "ContactName":
                    donors = donors.OrderBy(d => d.ContactName);
                    break;
                case "contactname_desc":
                    donors = donors.OrderByDescending(d => d.ContactName);
                    break;
                default:
                    donors = donors.OrderBy(d => d.OrganizationName);
                    break;
            }

            var sortedDonors = donors.Project().To<DonorListViewModel>();

            int pageSize = 3;
            int pageIndex = (page ?? 1);

            return View(sortedDonors.ToPagedList(pageIndex, pageSize));
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
