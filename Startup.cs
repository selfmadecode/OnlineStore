using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartStore.Startup))]
namespace SmartStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
