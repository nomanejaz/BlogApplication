using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Blog.Web.App_Start.Startup))]

namespace Blog.Web.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}