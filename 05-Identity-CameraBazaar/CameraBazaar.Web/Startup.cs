using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CameraBazaar.Web.Startup))]
namespace CameraBazaar.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
