namespace Charity.Web.Areas.Recipients.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using Charity.Common;
    using Charity.Data.Models;
    using Charity.Services;
    using Charity.Web.Infrastructure.Identity;
    using Charity.Web.Areas.Recipients.Models;

    [Authorize(Roles = GlobalConstants.RecipientRoleName)]
    public class ProfileController : Controller
    {
        private readonly RecipientProfileService recipientProfileService;
        private readonly CityService cityService;
        private readonly RecipientTypeService recipientTypeService;
        private readonly ICurrentUser currentUserProvider;

        public ProfileController(
            RecipientProfileService recipientProfileService, 
            CityService cityService, 
            RecipientTypeService recipientTypeService, 
            ICurrentUser currentUserProvider)
        {
            this.recipientProfileService = recipientProfileService;
            this.cityService = cityService;
            this.recipientTypeService = recipientTypeService;
            this.currentUserProvider = currentUserProvider;
        }
        
        public ActionResult Details()
        {
            ApplicationUser user = currentUserProvider.Get();
            Recipient recipient = this.recipientProfileService.GetByApplicationUserId(user.Id);

            if (recipient == null)
            {
                return HttpNotFound();
            }

            RecipientDetailsViewModel model = Mapper.Map<Recipient, RecipientDetailsViewModel>(recipient);
            model.AccountDetailsViewModel = Mapper.Map<ApplicationUser, AccountDetailsViewModel>(recipient.ApplicationUser);

            return View(model);
        }
                
        public ActionResult Edit()
        {
            ApplicationUser user = currentUserProvider.Get();
            Recipient recipient = this.recipientProfileService.GetByApplicationUserId(user.Id);

            if (recipient == null)
            {
                return HttpNotFound();
            }

            RecipientDetailsEditModel model = Mapper.Map<Recipient, RecipientDetailsEditModel>(recipient);
            model.AccountDetailsEditModel = Mapper.Map<ApplicationUser, AccountDetailsEditModel>(recipient.ApplicationUser);
            model.Cities = this.GetCities();
            model.RecipientTypes = this.GetRecipientTypes();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RecipientDetailsEditModel model)
        {
            ApplicationUser user = currentUserProvider.Get();
            Recipient recipient = this.recipientProfileService.GetByApplicationUserId(user.Id);
            
            if (ModelState.IsValid)
            {
                Mapper.Map<RecipientDetailsEditModel, Recipient>(model, recipient);
                Mapper.Map<AccountDetailsEditModel, ApplicationUser>(model.AccountDetailsEditModel, recipient.ApplicationUser);

                this.recipientProfileService.Update(recipient);

                return RedirectToAction("Details", "Profile", new { area = "Recipients"});
            }

            model.Cities = this.GetCities();
            model.RecipientTypes = this.GetRecipientTypes();

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

        private IEnumerable<SelectListItem> GetRecipientTypes()
        {
            var recipientTypes = this.recipientTypeService.GetAll()
                             .Select(r => new SelectListItem
                             {
                                 Value = r.Id.ToString(),
                                 Text = r.Name
                             });

            return new SelectList(recipientTypes, "Value", "Text");
        }
    }
}
