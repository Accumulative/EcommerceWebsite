using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MichellesWebsite.Startup))]
namespace MichellesWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
