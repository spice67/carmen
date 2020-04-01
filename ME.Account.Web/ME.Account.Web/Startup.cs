using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ME.Account.Web.Startup))]
namespace ME.Account.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
