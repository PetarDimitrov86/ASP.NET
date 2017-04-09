using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(LearningSystem.Api.Startup))]

namespace LearningSystem.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
