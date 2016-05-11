using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyBlogSite.Startup))]
namespace MyBlogSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
