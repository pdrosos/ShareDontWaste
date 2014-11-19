namespace Charity.Web.Areas.Donors.Controllers
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
    using Charity.Web.Areas.Donors.Models;
    using Charity.Web.Infrastructure.Identity;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
using System.Web;
using System.IO;

    [Authorize(Roles = GlobalConstants.DonorRoleName)]
    public class FoodDonationsController : Controller
    {
        private readonly IFoodDonationService foodDonationService;
        private readonly IFoodCategoryService foodCategoryService;
        private readonly IDonorProfileService donorProfileService;
        private readonly ICurrentUser currentUserProvider;

        public FoodDonationsController(
            IFoodDonationService foodDonationService,
            IFoodCategoryService foodCategoryService,
            IDonorProfileService donorProfileService,
            ICurrentUser currentUserProvider
            )
        {
            this.foodDonationService = foodDonationService;
            this.foodCategoryService = foodCategoryService;
            this.donorProfileService = donorProfileService;
            this.currentUserProvider = currentUserProvider;
        }

        //public ActionResult MyDonations()
        //{
        //    ApplicationUser user = this.currentUserProvider.Get();
        //    Donor donor = this.donorProfileService.GetByApplicationUserId(user.Id);
        //    Guid currentDonorId = donor.Id;

        //    var foodDonations = this.foodDonationService.All().Where(f => f.DonorId == currentDonorId).Project().To<FoodDonationListViewModel>();

        //    return View(foodDonations.AsEnumerable());
        //}

        public ActionResult MyDonations()
        {
            ViewData["foodcategories"] = this.foodCategoryService.GetAll().Project().To<FoodCategoryViewModel>();
            return View();
        }

        public ActionResult ReadMyDonations([DataSourceRequest] DataSourceRequest request)
        {
            ApplicationUser user = this.currentUserProvider.Get();
            Donor donor = this.donorProfileService.GetByApplicationUserId(user.Id);
            Guid currentDonorId = donor.Id;
            
            var foodDonations = this.foodDonationService.All()
                .Where(f => f.DonorId == currentDonorId)
                .Project().To<FoodDonationListViewModel>();

            return Json(foodDonations.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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

        public ActionResult Create()
        {
            ViewBag.FoodCategoryId = new SelectList(this.foodCategoryService.GetAll(), "Id", "Name");
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FoodDonationRegisterModel model)
        {
            List<string> validImageTypes = new List<string>()
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            if (ModelState.IsValid)
            {
                if (model.ImageUpload != null && !validImageTypes.Contains(model.ImageUpload.ContentType))
                {
                    ModelState.AddModelError("", "Please choose either a GIF, JPG or PNG image.");
                    ViewBag.FoodCategoryId = new SelectList(this.foodCategoryService.GetAll(), "Id", "Name", model.FoodCategoryId);
                    return View(model);
                }

                if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                {
                    string uploadDir = "/Content/Images/Donations_Food";
                    //string imagePath = Path.Combine(Server.MapPath(uploadDir), model.ImageUpload.FileName);

                    string extension = Path.GetExtension(model.ImageUpload.FileName);

                    string imageFileName = String.Format(
                        "Food-Donations-{0}{1}",
                        DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"), extension);

                    string imagePath = Path.Combine(Server.MapPath(uploadDir), imageFileName);
                    model.ImageUrl = uploadDir + "/" + imageFileName;
                    model.ImageUpload.SaveAs(imagePath);
                }

                ApplicationUser user = this.currentUserProvider.Get();
                Donor donor = this.donorProfileService.GetByApplicationUserId(user.Id);

                FoodDonation foodDonation = Mapper.Map<FoodDonationRegisterModel, FoodDonation>(model);

                foodDonation.DonorId = donor.Id;

                this.foodDonationService.Add(foodDonation);
                return RedirectToAction("MyDonations");
            }

            ViewBag.FoodCategoryId = new SelectList(this.foodCategoryService.GetAll(), "Id", "Name", model.FoodCategoryId);
            return View(model);
        }

        // GET: Donors/FoodDonations/Edit/5
        public ActionResult Edit(int? id)
        {
            ApplicationUser user = this.currentUserProvider.Get();
            Donor donor = this.donorProfileService.GetByApplicationUserId(user.Id);
            FoodDonation foodDonation = this.foodDonationService.GetById((int)id);

            if (donor.Id != foodDonation.DonorId)
            {
                return RedirectToAction("Index");
            }

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
                return RedirectToAction("MyDonations");
            }

            IEnumerable<FoodCategory> foodCategories = this.foodCategoryService.GetAll();
            ViewBag.FoodCategoryId = new SelectList(foodCategories, "Id", "Name", foodDonation.FoodCategoryId);
            return View(foodDonation);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateDonation([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<FoodDonationListViewModel> foodDonations)
        {
            if (foodDonations != null && ModelState.IsValid)
            {
                foreach (var modelDonation in foodDonations)
                {
                    var donation = this.foodDonationService.GetById(modelDonation.Id);

                    Mapper.Map<FoodDonationListViewModel, FoodDonation>(modelDonation, donation);
                    //donation.FoodCategory = this.foodCategoryService.GetById(modelDonation.FoodCategory.Id);
                    donation.FoodCategoryId = modelDonation.Category.Id;
                    this.foodDonationService.Update(donation);
                }
            }   

            return Json(foodDonations.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteDonation([DataSourceRequest] DataSourceRequest request,
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
            return RedirectToAction("MyDonations");
        }

        private IEnumerable<SelectListItem> GetFoodCategories()
        {
            var foodCategories = this.foodCategoryService.GetAll()
                             .Select(f => new SelectListItem
                             {
                                 Value = f.Id.ToString(),
                                 Text = f.Name
                             });

            return new SelectList(foodCategories, "Value", "Text");
        }

        private string UploadImage(HttpPostedFileBase image)
        {
            string imageUrl = string.Empty;
            List<string> validImageTypes = new List<string>()
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            if (image == null || image.ContentLength == 0)
            {
                ModelState.AddModelError("", "This field is required");
            }
            else if (!validImageTypes.Contains(image.ContentType))
            {
                ModelState.AddModelError("", "Please choose either a GIF, JPG or PNG image.");
            }

            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    var uploadDir = "~/Content/Images/Donations_Food";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), image.FileName);
                    imageUrl = Path.Combine(uploadDir, image.FileName);
                    image.SaveAs(imagePath);
                }
            }

            return imageUrl;
        }
    }
}
