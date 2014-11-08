using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Charity.Web.Startup))]
namespace Charity.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
