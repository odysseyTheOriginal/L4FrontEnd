using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(L4ProjectFrontEnd.Startup))]
namespace L4ProjectFrontEnd
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
