using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShopMVC.Startup))]
namespace ShopMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
