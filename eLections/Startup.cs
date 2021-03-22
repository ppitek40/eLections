using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eLections.Startup))]
namespace eLections
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
