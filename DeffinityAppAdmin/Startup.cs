using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(DeffinityAppAdmin.Startup))]

namespace DeffinityAppAdmin
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
