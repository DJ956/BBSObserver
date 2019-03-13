using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BBSObserver.Startup))]
namespace BBSObserver
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
