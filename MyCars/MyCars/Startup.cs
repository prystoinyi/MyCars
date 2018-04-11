using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyCars.Startup))]
namespace MyCars
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            InitDB();
        }
    }
}
