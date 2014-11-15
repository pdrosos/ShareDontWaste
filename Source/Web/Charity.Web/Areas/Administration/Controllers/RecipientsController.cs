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
    using Charity.Services.Common;
    using Charity.Web.Areas.Administration.Models;
    using PagedList;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class RecipientsController : Controller
    {
        private readonly IRecipientProfileService recipientProfileService;
        private readonly ICityService cityService;
        private readonly IRecipientTypeService recipientTypeService;

        public RecipientsController(
            IRecipientProfileService recipientProfileService,
            IRecipientTypeService recipientTypeService,
            ICityService cityService)
        {
            this.recipientProfileService = recipientProfileService;
            this.recipientTypeService = recipientTypeService;
            this.cityService = cityService;
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.OrganizationNameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.RecipientTypeSortParam = sortOrder == "RecipientType" ? "recipienttype_desc" : "RecipientType";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var recipients = this.recipientProfileService.All();

            if (!String.IsNullOrEmpty(searchString))
            {
                recipients = recipients.Where(r => r.OrganizationName.Contains(searchString)
                                       || r.RecipientType.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    recipients = recipients.OrderByDescending(r => r.OrganizationName);
                    break;
                case "RecipientType":
                    recipients = recipients.OrderBy(r => r.RecipientType.Name);
                    break;
                case "recipienttype_desc":
                    recipients = recipients.OrderByDescending(r => r.RecipientType.Name);
                    break;
                default:
                    recipients = recipients.OrderBy(r => r.OrganizationName);
                    break;
            }

            var sortedRecipients = recipients.Project().To<RecipientListViewModel>();

            int pageSize = 10;
            int pageIndex = (page ?? 1);

            return View(sortedRecipients.ToPagedList(pageIndex, pageSize));
        }

        //public ActionResult Index(int? page)
        //{
        //    var allRecipients = this.recipientProfileService.All().Project().To<RecipientListViewModel>();

        //    int pageSize = 3;
        //    int pageIndex = (page ?? 1);

        //    return View(allRecipients.OrderBy(o => o.OrganizationName).ToPagedList(pageIndex, pageSize));
        //}

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
