using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Charity.Data;
using Charity.Data.Models;
using Charity.Web.Areas.Donors.Models;
using AutoMapper;
using Microsoft.AspNet.Identity;

namespace Charity.Web.Areas.Donors.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Donors/Profile/Details/5

        [Authorize(Roles = "Donor")]
        public ActionResult Details([Bind(Prefix = "id")] string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Donor donor = this.donorService.GetByUserName(username);
            ApplicationUser user = db.Users.Where(u => u.UserName == username).FirstOrDefault();
            Donor donor = db.Donors.Where(d => d.ApplicationUserId == user.Id).FirstOrDefault();

            if (donor == null)
            {
                return HttpNotFound();
            }

            DonorDetailsViewModel model = Mapper.Map<Donor, DonorDetailsViewModel>(donor);
            model.AccountDetailsViewModel = Mapper.Map<ApplicationUser, AccountDetailsViewModel>(donor.ApplicationUser);

            return View(model);
        }

        // GET: Donors/Profile/Edit/username
        [Authorize(Roles = "Donor")]
        public ActionResult Edit(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Donor donor = this.donorService.GetByUserName(username);
            ApplicationUser user = db.Users.Where(u => u.UserName == username).FirstOrDefault();
            Donor donor = db.Donors.Where(d => d.ApplicationUserId == user.Id).FirstOrDefault();

            if (donor == null)
            {
                return HttpNotFound();
            }

            DonorDetailsViewModel model = Mapper.Map<Donor, DonorDetailsViewModel>(donor);
            model.AccountDetailsViewModel = Mapper.Map<ApplicationUser, AccountDetailsViewModel>(donor.ApplicationUser);

            return View(model);
        }

        // POST: Donors/Profile/Edit/username
        [Authorize(Roles = "Donor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DonorDetailsViewModel model)
        {
            //Donor donor = this.donorService.GetById(model.Id);
            ApplicationUser user = db.Users
                .Where(u => u.UserName == model.AccountDetailsViewModel.UserName)
                .FirstOrDefault();

            Donor donor = db.Donors.Where(d => d.ApplicationUserId == user.Id).FirstOrDefault();

            //bool isUserNameUnique = this.donorService.IsUserNameUniqueOnEdit(donor, model.AccountDetailsViewModel.UserName);

            //if (!isUserNameUnique)
            //{
            //    this.ModelState.AddModelError("AccountDetailsViewModel.UserName", "Duplicate usernames are not allowed.");
            //}

            if (ModelState.IsValid)
            {
                Mapper.Map<DonorDetailsViewModel, Donor>(model, donor);
                Mapper.Map<AccountDetailsViewModel, ApplicationUser>(model.AccountDetailsViewModel, donor.ApplicationUser);
                
                //this.donorService.Update(donor);
                db.Entry(donor).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "Home", new { area = ""});
            }
            return View(model);
        }

        // GET: Donors/Profile/Delete/username

        [Authorize(Roles = "Donor")]
        public ActionResult Delete(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Donor donor = this.donorService.GetByUserName(username);
            ApplicationUser user = db.Users.Where(u => u.UserName == username).FirstOrDefault();
            Donor donor = db.Donors.Where(d => d.ApplicationUserId == user.Id).FirstOrDefault();

            if (donor == null)
            {
                return HttpNotFound();
            }

            DonorDetailsViewModel model = Mapper.Map<Donor, DonorDetailsViewModel>(donor);
            model.AccountDetailsViewModel = Mapper.Map<ApplicationUser, AccountDetailsViewModel>(donor.ApplicationUser);

            return View(model);
        }

        // POST: Donors/Profile/Delete/username

        [Authorize(Roles = "Donor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Donor donor = this.donorService.GetByUserName(username);
            ApplicationUser user = db.Users.Where(u => u.UserName == username).FirstOrDefault();
            Donor donor = db.Donors.Where(d => d.ApplicationUserId == user.Id).FirstOrDefault();

            if (donor == null)
            {
                return HttpNotFound();
            }

            if (donor.ApplicationUserId == User.Identity.GetUserId())
            {
                //donor.DeletedBy = User.Identity.GetUserId();
                //this.donorService.Delete(donor);

                //var accountController = new AccountController(this.donorService);
                //accountController.ControllerContext = this.ControllerContext;
                //accountController.LogOff();

                return RedirectToAction("Index", "Home", new { area = ""});
            }

            //donor.DeletedBy = User.Identity.GetUserId();
            //this.donorService.Delete(donor);

            db.Donors.Remove(donor);
            db.SaveChanges();

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
