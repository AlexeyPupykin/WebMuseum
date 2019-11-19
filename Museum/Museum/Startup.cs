using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Museum.Startup))]
namespace Museum
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
