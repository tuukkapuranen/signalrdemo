using Microsoft.Owin;
using Owin;
using SignalRDemo;

// ReSharper disable UnusedMember.Global

[assembly: OwinStartup( typeof( Startup ) )]
namespace SignalRDemo
{
    public class Startup
    {        
        public void Configuration( IAppBuilder app )
        {
            app.MapSignalR();
        }
    }
}