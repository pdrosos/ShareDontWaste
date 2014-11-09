using System.Web.Mvc;

namespace Charity.Web.Areas.Donors
{
    public class DonorsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Donors";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "Donors_default",
                url: "Donors/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Charity.Web.Areas.Donors.Controllers" }
            );
        }
    }
}