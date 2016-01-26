using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HackerFinder.Web.Startup))]
namespace HackerFinder.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
