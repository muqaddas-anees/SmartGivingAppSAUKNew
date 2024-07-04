using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(DeffinityAppDev.Startup))]

namespace DeffinityAppDev
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Enable CORS for all origins, methods, and headers
            app.UseCors(CorsOptions.AllowAll);
            // Map SignalR hubs
            app.MapSignalR();
        }
    }
}
