using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MarCom.Presentation.Startup))]
namespace MarCom.Presentation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
