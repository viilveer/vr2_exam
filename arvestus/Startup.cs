using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Arvestus.Startup))]
namespace Arvestus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
