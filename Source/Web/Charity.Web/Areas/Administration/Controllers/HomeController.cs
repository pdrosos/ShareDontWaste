namespace Charity.Web.Areas.Administration.Controllers
{
    using Charity.Common;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}