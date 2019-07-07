using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GpProject.Startup))]
namespace GpProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
