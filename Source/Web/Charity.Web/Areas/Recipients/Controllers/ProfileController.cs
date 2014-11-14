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
        private readonly FoodCategoryService foodCategoryService;
        private readonly ICurrentUser currentUserProvider;

        public ProfileController(
            RecipientProfileService recipientProfileService, 
            CityService cityService,
            RecipientTypeService recipientTypeService,
            FoodCategoryService foodCategoryService,
            ICurrentUser currentUserProvider)
        {
            this.recipientProfileService = recipientProfileService;
            this.cityService = cityService;
            this.recipientTypeService = recipientTypeService;
            this.foodCategoryService = foodCategoryService;
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
            model.RecipientTypes = new SelectList(this.recipientTypeService.GetAll(), "Id", "Name");
            model.FoodCategories = this.GetFoodCategories(recipient);

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

                var selectedCategories = model.FoodCategories.Where(c => c.IsChecked);
                recipient.FoodCategories.Clear();
                foreach (var categoryModel in selectedCategories)
                {
                    var category = this.foodCategoryService.GetById(categoryModel.Id);
                    recipient.FoodCategories.Add(category);
                }

                this.recipientProfileService.Update(recipient);

                return RedirectToAction("Details", "Profile", new { area = "Recipients"});
            }

            model.Cities = this.GetCities();
            model.RecipientTypes = new SelectList(this.recipientTypeService.GetAll(), "Id", "Name");
            model.FoodCategories = this.GetFoodCategories(recipient);

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

        /// <summary>
        /// Manual checkbox list generation
        /// http://stackoverflow.com/questions/12808936/how-do-i-render-a-group-of-checkboxes-using-mvc-4-and-view-models-strongly-type
        /// TODO: Improved checkbox list with
        /// https://www.nuget.org/packages/MvcCheckBoxList/ (http://mvccbl.com/Examples) or 
        /// https://www.nuget.org/packages/Hex/ (http://staticdotnet.wordpress.com/hex/hex-creating-a-list-of-checkboxes/)
        /// </summary>
        /// <param name="recipient"></param>
        /// <returns></returns>
        private IList<FoodCategoryEditModel> GetFoodCategories(Recipient recipient)
        {
            var recipientSelectedCategoriesIds = new HashSet<int>();
            foreach (var recipientCategory in recipient.FoodCategories)
            {
                recipientSelectedCategoriesIds.Add(recipientCategory.Id);
            }

            var foodCategories = this.foodCategoryService.GetAll();
            var foodCategoriesList = new List<FoodCategoryEditModel>();

            foreach (var foodCategory in foodCategories)
            {
                var foodCategoryModel = new FoodCategoryEditModel();
                foodCategoryModel.Id = foodCategory.Id;
                foodCategoryModel.Name = foodCategory.Name;

                if (recipientSelectedCategoriesIds.Contains(foodCategory.Id))
                {
                    foodCategoryModel.IsChecked = true;
                }

                foodCategoriesList.Add(foodCategoryModel);
            }

            return foodCategoriesList;
        }
    }
}
