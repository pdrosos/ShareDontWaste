namespace Charity.Web.Areas.Donors.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using Charity.Common;
    using Charity.Data.Models;
    using Charity.Services;
    using Charity.Web.Areas.Donors.Models;
    using Charity.Web.Infrastructure.Identity;

    [Authorize(Roles = GlobalConstants.DonorRoleName)]
    public class ProfileController : Controller
    {
        private readonly DonorProfileService donorProfileService;
        private readonly CityService cityService;
        private readonly ICurrentUser currentUserProvider;

        public ProfileController(DonorProfileService donorProfileService, CityService cityService, ICurrentUser currentUserProvider)
        {
            this.donorProfileService = donorProfileService;
            this.cityService = cityService;
            this.currentUserProvider = currentUserProvider;
        }
        
        public ActionResult Details()
        {
            ApplicationUser user = currentUserProvider.Get();
            Donor donor = this.donorProfileService.GetByApplicationUserId(user.Id);

            if (donor == null)
            {
                return HttpNotFound();
            }

            DonorDetailsViewModel model = Mapper.Map<Donor, DonorDetailsViewModel>(donor);
            model.AccountDetailsViewModel = Mapper.Map<ApplicationUser, AccountDetailsViewModel>(donor.ApplicationUser);

            return View(model);
        }
                
        public ActionResult Edit()
        {
            ApplicationUser user = currentUserProvider.Get();
            Donor donor = this.donorProfileService.GetByApplicationUserId(user.Id);

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
            ApplicationUser user = currentUserProvider.Get();
            Donor donor = this.donorProfileService.GetByApplicationUserId(user.Id);
            
            if (ModelState.IsValid)
            {
                Mapper.Map<DonorDetailsEditModel, Donor>(model, donor);
                Mapper.Map<AccountDetailsEditModel, ApplicationUser>(model.AccountDetailsEditModel, donor.ApplicationUser);
                
                this.donorProfileService.Update(donor);

                return RedirectToAction("Details", "Profile", new { area = "Donors"});
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
