using System;
using System.Configuration;
using System.Threading.Tasks;
using MyBlogSite.Domain.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using MyBlogSite.Domain.Models.DBContext;

namespace MyBlogSite.Domain.Identity
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class MyBlogSiteUserManager : UserManager<MyBlogSiteUser>
    {
        public MyBlogSiteUserManager(IUserStore<MyBlogSiteUser> store)
            : base(store)
        {
        }

        public static MyBlogSiteUserManager Create(IdentityFactoryOptions<MyBlogSiteUserManager> options, IOwinContext context)
        {
            var manager = new MyBlogSiteUserManager(new UserStore<MyBlogSiteUser>(context.Get<MyBlogSiteDatabaseContext>()));

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<MyBlogSiteUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };

            // Configure user lockout defaults
            bool lockoutSetting;
            manager.UserLockoutEnabledByDefault = true;
            if (bool.TryParse(ConfigurationManager.AppSettings.Get("UserLockoutEnabledByDefault"), out lockoutSetting))
                manager.UserLockoutEnabledByDefault = lockoutSetting;

            int lockoutTimeSpan;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            if (int.TryParse(ConfigurationManager.AppSettings.Get("DefaultAccountLockoutTimeSpan"), out lockoutTimeSpan))
                manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(lockoutTimeSpan);

            int maxFailedAttempts;
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
            if (int.TryParse(ConfigurationManager.AppSettings.Get("MaxFailedAccessAttemptsBeforeLockout"),
                out maxFailedAttempts))
                manager.MaxFailedAccessAttemptsBeforeLockout = maxFailedAttempts;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code",
                new PhoneNumberTokenProvider<MyBlogSiteUser> {MessageFormat = "Your security code is {0}"});
            manager.RegisterTwoFactorProvider("Email Code",
                new EmailTokenProvider<MyBlogSiteUser> {Subject = "Security Code", BodyFormat = "Your security code is {0}"});


            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<MyBlogSiteUser>(dataProtectionProvider.Create("ASP.NET Identity"))
                    {
                        TokenLifespan = TimeSpan.FromHours(24)
                    };
            }

            return manager;
        }
        public static async Task<MyBlogSiteUserManager> CreateAsync(IdentityFactoryOptions<MyBlogSiteUserManager> options, IOwinContext context)
        {
            var manager = new MyBlogSiteUserManager(new UserStore<MyBlogSiteUser>(context.Get<MyBlogSiteDatabaseContext>()));

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<MyBlogSiteUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };

            // Configure user lockout defaults
            bool lockoutSetting;
            manager.UserLockoutEnabledByDefault = true;
            if (bool.TryParse(ConfigurationManager.AppSettings.Get("UserLockoutEnabledByDefault"), out lockoutSetting))
                manager.UserLockoutEnabledByDefault = lockoutSetting;

            int lockoutTimeSpan;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            if (int.TryParse(ConfigurationManager.AppSettings.Get("DefaultAccountLockoutTimeSpan"), out lockoutTimeSpan))
                manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(lockoutTimeSpan);

            int maxFailedAttempts;
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
            if (int.TryParse(ConfigurationManager.AppSettings.Get("MaxFailedAccessAttemptsBeforeLockout"),
                out maxFailedAttempts))
                manager.MaxFailedAccessAttemptsBeforeLockout = maxFailedAttempts;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code",
                new PhoneNumberTokenProvider<MyBlogSiteUser> { MessageFormat = "Your security code is {0}" });
            manager.RegisterTwoFactorProvider("Email Code",
                new EmailTokenProvider<MyBlogSiteUser> { Subject = "Security Code", BodyFormat = "Your security code is {0}" });


            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<MyBlogSiteUser>(dataProtectionProvider.Create("ASP.NET Identity"))
                    {
                        TokenLifespan = TimeSpan.FromHours(24)
                    };
            }

            return manager;
        }
    }
}