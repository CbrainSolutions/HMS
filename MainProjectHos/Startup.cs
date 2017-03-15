using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MainProjectHos.Startup))]
namespace MainProjectHos
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
