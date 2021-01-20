using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Space101.Startup))]
namespace Space101
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            app.MapSignalR();
        }
    }
}