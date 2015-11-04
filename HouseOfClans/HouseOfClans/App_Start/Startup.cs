using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HouseOfClans.Startup))]
namespace HouseOfClans
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
