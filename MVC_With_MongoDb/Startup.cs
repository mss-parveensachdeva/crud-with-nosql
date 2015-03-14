using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_With_MongoDb.Startup))]
namespace MVC_With_MongoDb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
