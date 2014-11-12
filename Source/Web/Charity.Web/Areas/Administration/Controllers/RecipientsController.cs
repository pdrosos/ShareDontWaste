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
    using Charity.Web.Infrastructure.Identity;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class RecipientsController : Controller
    {
        private readonly RecipientProfileService recipientProfileService;
        private readonly CityService cityService;
        private readonly RecipientTypeService recipientTypeService;
        private readonly ICurrentUser currentUserProvider;

        public RecipientsController(
            RecipientProfileService recipientProfileService,
            RecipientTypeService recipientTypeService,
            CityService cityService,
            ICurrentUser currentUserProvider)
        {
            this.recipientProfileService = recipientProfileService;
            this.recipientTypeService = recipientTypeService;
            this.cityService = cityService;
            this.currentUserProvider = currentUserProvider;
        }


        public ActionResult Index()
        {
            var allRecipients = this.recipientProfileService.All().Project().To<RecipientListViewModel>();
            IEnumerable<RecipientListViewModel> allRecipientsEnumerable = allRecipients.AsEnumerable();
            return View(allRecipientsEnumerable);
        }

        public ActionResult Details(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Recipient recipient = this.recipientProfileService.GetByUserName(username);

            if (recipient == null)
            {
                return HttpNotFound();
            }

            RecipientDetailsViewModel model = Mapper.Map<Recipient, RecipientDetailsViewModel>(recipient);
            model.AccountDetailsViewModel = Mapper.Map<ApplicationUser, AccountDetailsViewModel>(recipient.ApplicationUser);

            return View(model);
        }

        public ActionResult Edit(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Recipient recipient = this.recipientProfileService.GetByUserName(username);

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

        // POST: Donors/Profile/Edit/username
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RecipientDetailsEditModel model)
        {
            //ApplicationUser user = currentUserProvider.Get();
            Recipient recipient = this.recipientProfileService.GetById(model.Id);
            
            if (ModelState.IsValid)
            {
                Mapper.Map<RecipientDetailsEditModel, Recipient>(model, recipient);
                Mapper.Map<AccountDetailsEditModel, ApplicationUser>(model.AccountDetailsEditModel, recipient.ApplicationUser);

                this.recipientProfileService.Update(recipient);

                return RedirectToAction("Index", "Recipients");
            }

            model.Cities = this.GetCities();
            model.RecipientTypes = this.GetRecipientTypes();

            return View(model);
        }

        [HttpGet]
        public ActionResult Search(string name)
        {
            var foundRecipients = this.recipientProfileService
                .SearchByOrganizationName(name)
                .Project()
                .To<RecipientListViewModel>();

            return View(foundRecipients.AsEnumerable());
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
