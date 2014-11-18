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
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class FoodDonationsController : Controller
    {
        private readonly IFoodDonationService foodDonationService;
        private readonly IFoodCategoryService foodCategoryService;

        public FoodDonationsController(IFoodDonationService foodDonationService, IFoodCategoryService foodCategoryService)
        {
            this.foodDonationService = foodDonationService;
            this.foodCategoryService = foodCategoryService;
        }

        public ActionResult Index()
        {
            ViewData["foodcategories"] = this.foodCategoryService.GetAll().Project().To<FoodCategoryViewModel>();
            return View();
        }

        public ActionResult ReadDonations([DataSourceRequest] DataSourceRequest request)
        {
            var allDonations = this.foodDonationService.All().Project().To<FoodDonationListViewModel>();
            return Json(allDonations.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateDonations([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<FoodDonationListViewModel> foodDonations)
        {
            if (foodDonations != null && ModelState.IsValid)
            {
                foreach (var donationModel in foodDonations)
                {
                    var donation = this.foodDonationService.GetById(donationModel.Id);

                    Mapper.Map<FoodDonationListViewModel, FoodDonation>(donationModel, donation);
                    donation.FoodCategoryId = donationModel.Category.Id;
                    this.foodDonationService.Update(donation);
                }
            }

            return Json(foodDonations.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteDonations([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<FoodDonationListViewModel> foodDonations)
        {
            if (foodDonations != null && ModelState.IsValid)
            {
                foreach (var modelDonation in foodDonations)
                {
                    var donation = this.foodDonationService.GetById(modelDonation.Id);

                    Mapper.Map<FoodDonationListViewModel, FoodDonation>(modelDonation, donation);
                    donation.FoodCategoryId = modelDonation.Category.Id;
                    this.foodDonationService.Delete(donation);
                }
            }

            return Json(foodDonations.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FoodDonation foodDonation = this.foodDonationService.GetById((int)id);

            if (foodDonation == null)
            {
                return HttpNotFound();
            }

            FoodDonationViewModel model = Mapper.Map<FoodDonation, FoodDonationViewModel>(foodDonation);

            return View(model);
        }

        // GET: Donors/FoodDonations/Edit/5
        public ActionResult Edit(int? id)
        {
            FoodDonation foodDonation = this.foodDonationService.GetById((int)id);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (foodDonation == null)
            {
                return HttpNotFound();
            }

            IEnumerable<FoodCategory> foodCategories = this.foodCategoryService.GetAll();

            ViewBag.FoodCategoryId = new SelectList(foodCategories, "Id", "Name", foodDonation.FoodCategoryId);

            FoodDonationEditModel model = Mapper.Map<FoodDonation, FoodDonationEditModel>(foodDonation);

            return View(model);
        }

        // POST: Donors/FoodDonations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FoodDonationEditModel model)
        {
            FoodDonation foodDonation = this.foodDonationService.GetById(model.Id);

            Mapper.Map<FoodDonationEditModel, FoodDonation>(model, foodDonation);

            if (ModelState.IsValid)
            {
                this.foodDonationService.Update(foodDonation);

                this.Flash("Donation is updated", FlashEnum.Success);

                return RedirectToAction("Index");
            }

            IEnumerable<FoodCategory> foodCategories = this.foodCategoryService.GetAll();
            ViewBag.FoodCategoryId = new SelectList(foodCategories, "Id", "Name", foodDonation.FoodCategoryId);
            return View(foodDonation);
        }

        // GET: Donors/FoodDonations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodDonation foodDonation = this.foodDonationService.GetById((int)id);
            if (foodDonation == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<FoodDonation, FoodDonationViewModel>(foodDonation);

            return View(model);
        }

        // POST: Donors/FoodDonations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodDonation foodDonation = this.foodDonationService.GetById(id);
            this.foodDonationService.Delete(foodDonation);
            return RedirectToAction("Index");
        }
    }
}