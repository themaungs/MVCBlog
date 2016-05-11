using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using MyBlogSite.Domain.Models;
using MyBlogSite.Domain.Identity;

namespace MyBlogSite.App_Start.IdentityConfig
{

    public class MyBlogSiteSignInManager : SignInManager<MyBlogSiteUser, string>
    {
        public MyBlogSiteSignInManager(MyBlogSiteUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(MyBlogSiteUser user)
        {
            return user.GenerateUserIdentityAsync((MyBlogSiteUserManager)UserManager);
        }

        public static MyBlogSiteSignInManager Create(IdentityFactoryOptions<MyBlogSiteSignInManager> options,
            IOwinContext context)
        {
            return new MyBlogSiteSignInManager(context.GetUserManager<MyBlogSiteUserManager>(),
                context.Authentication);
        }
        public static async Task<MyBlogSiteSignInManager> CreateAsync(IdentityFactoryOptions<MyBlogSiteSignInManager> options,
            IOwinContext context)
        {
            return new MyBlogSiteSignInManager(context.GetUserManager<MyBlogSiteUserManager>(),
                context.Authentication);
        }
    }
}