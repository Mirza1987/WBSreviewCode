using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DigitArchive.Startup))]
namespace DigitArchive
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
