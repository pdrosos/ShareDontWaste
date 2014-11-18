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
    public class FoodRequestsController : Controller
    {
        private readonly IFoodRequestService foodRequestService;

        public FoodRequestsController(IFoodRequestService foodRequestService)
        {
            this.foodRequestService = foodRequestService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReadRequests([DataSourceRequest] DataSourceRequest request)
        {
            var allRequests = this.foodRequestService.All().OrderByDescending(r => r.Id).Project().To<FoodRequestListViewModel>();
            return Json(allRequests.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateRequests([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<FoodRequestListViewModel> foodRequests)
        {
            if (foodRequests != null && ModelState.IsValid)
            {
                foreach (var foodRequestModel in foodRequests)
                {
                    var foodRequest = this.foodRequestService.GetById(foodRequestModel.Id);

                    Mapper.Map<FoodRequestListViewModel, FoodRequest>(foodRequestModel, foodRequest);
                    this.foodRequestService.Update(foodRequest);
                }
            }

            return Json(foodRequests.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteRequests([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<FoodRequestListViewModel> foodRequests)
        {
            if (foodRequests != null && ModelState.IsValid)
            {
                foreach (var foodRequestModel in foodRequests)
                {
                    var foodRequest = this.foodRequestService.GetById(foodRequestModel.Id);

                    Mapper.Map<FoodRequestListViewModel, FoodRequest>(foodRequestModel, foodRequest);
                    this.foodRequestService.Delete(foodRequest);
                }
            }

            return Json(foodRequests.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FoodRequest foodRequest = this.foodRequestService.GetById((int)id);

            if (foodRequest == null)
            {
                return HttpNotFound();
            }

            FoodRequestViewModel model = Mapper.Map<FoodRequest, FoodRequestViewModel>(foodRequest);

            return View(model);
        }

        // GET: Administration/FoodRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            FoodRequest foodRequest = this.foodRequestService.GetById((int)id);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (foodRequest == null)
            {
                return HttpNotFound();
            }

            FoodRequestEditModel model = Mapper.Map<FoodRequest, FoodRequestEditModel>(foodRequest);

            return View(model);
        }

        // POST: Administration/FoodRequests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FoodRequestEditModel model)
        {
            FoodRequest foodRequest = this.foodRequestService.GetById(model.Id);

            Mapper.Map<FoodRequestEditModel, FoodRequest>(model, foodRequest);

            if (ModelState.IsValid)
            {
                this.foodRequestService.Update(foodRequest);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Administration/FoodRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            FoodRequest foodRequest = this.foodRequestService.GetById((int)id);
            if (foodRequest == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<FoodRequest, FoodRequestViewModel>(foodRequest);

            return View(model);
        }

        // POST: Administration/FoodRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodRequest foodRequest = this.foodRequestService.GetById(id);
            this.foodRequestService.Delete(foodRequest);
            return RedirectToAction("Index");
        }
    }
}