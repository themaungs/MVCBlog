using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using MyBlogSite.Domain.Models;
using MyBlogSite.Domain.Properties;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyBlogSite.Domain.Models.DBContext;
using System.Threading.Tasks;

namespace MyBlogSite.Domain.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<MyBlogSiteDatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MyBlogSiteDatabaseContext context)
        {
            SeedRoles(context);
            SeedCountries(context);
            SeedUsers(context);
            SeedBlogCategory(context);
            SeedPages(context);
            SeedBlog(context);
        }

        // ----- This method populates DB with default roles
        private void SeedRoles(MyBlogSiteDatabaseContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists("Super-User"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Super-User"));
            }

            if (!roleManager.RoleExists("Editor"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Editor"));
            }

            if (!roleManager.RoleExists("Blogger"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Blogger"));
            }
        }

        // ----- This method looks at CSV file in SeedData folder and populates the DB
        private void SeedCountries(MyBlogSiteDatabaseContext context)
        {
            var co = new Country
            {
                Name = "United States"
            };
            context.Countries.Add(co);
            context.SaveChanges();
        }

        // ----- This method populates DB with default admin account
        private void SeedUsers(MyBlogSiteDatabaseContext context)
        {
            var userManager = new UserManager<MyBlogSiteUser>(new UserStore<MyBlogSiteUser>(context));

            if (userManager.FindByEmail("admin@myblogsite.com") == null)
            {
                var blogSuperUser = new MyBlogSiteUser
                {
                    FirstName = "Admin",
                    LastName = "Blogsite",
                    UserName = "admin@myblogsite.com",
                    Email = "admin@myblogsite.com",
                    AccessFailedCount = 0,
                    CreatedDate = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow,
                    CreatedBy = "seed",
                    LastUpdatedBy = "seed",
                    PhoneNumber = "888-888-8888",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    State = "Illinois",
                    CountryId = context.Countries.Where(c => c.Name == "United States").Select(c => c.Id).First()
                };
                var createAdminResult = userManager.Create(blogSuperUser, "MyBlog$it3");
                if (createAdminResult.Succeeded)
                {
                    var result = userManager.AddToRole(blogSuperUser.Id, "Super-User");
                }
            }
            context.SaveChanges();
        }

        // ----- This method populates DB with default Category
        private void SeedBlogCategory(MyBlogSiteDatabaseContext context)
        {
            var category = context.BlogCategories.FirstOrDefault(c => c.Name == "General");
            if (category != null)
                return;
            category = new BlogCategory
            {
                BlogCategoryId = Guid.NewGuid(),
                Name = "General",
                CreatedBy = "seed",
                CreatedDate = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow,
                LastUpdatedBy = "seed"
            };
            context.BlogCategories.Add(category);
            context.SaveChanges();
        }

        // ----- This method populates DB with default Pages
        private void SeedPages(MyBlogSiteDatabaseContext context)
        {
            AddPage(context, "About Me", "icon-user", "/content/images/pic13.jpg", 
                            "Donec eget ex magna. Interdum et malesuada fames ac ante ipsum primis in faucibus. Pellentesque venenatis dolor imperdiet dolor mattis sagittis. Praesent rutrum sem diam, vitae egestas enim auctor sit amet. Pellentesque leo mauris, consectetur id ipsum sit amet, fergiat. Pellentesque in mi eu massa lacinia malesuada et a elit. Donec urna ex, lacinia in purus ac, pretium pulvinar mauris. Curabitur sapien risus, commodo eget turpis at, elementum convallis elit. Pellentesque enim turpis, hendrerit tristique.",
                            10, false);
            AddPage(context, "Contact", "icon-envelope", "/content/images/pic13.jpg",
                            "Donec eget ex magna. Interdum et malesuada fames ac ante ipsum primis in faucibus. Pellentesque venenatis dolor imperdiet dolor mattis sagittis. Praesent rutrum sem diam, vitae egestas enim auctor sit amet. Pellentesque leo mauris, consectetur id ipsum sit amet, fergiat. Pellentesque in mi eu massa lacinia malesuada et a elit. Donec urna ex, lacinia in purus ac, pretium pulvinar mauris. Curabitur sapien risus, commodo eget turpis at, elementum convallis elit. Pellentesque enim turpis, hendrerit tristique.",
                             3, true);
        }
        private void AddPage(MyBlogSiteDatabaseContext context, string Title, string PageIcon, string HeroImage, string HtmlContent, int OrderRanking, bool ShowPageDetails)
        {
            var page = context.Pages.FirstOrDefault(c => c.Title == Title);
            if (page != null)
            {
                page.PageIcon = PageIcon;
                //page.HtmlContent = HtmlContent;//Comment out
                page.HeroImage = HeroImage;
                page.OrderRanking = OrderRanking;
                page.ShowPageDetails = ShowPageDetails;
                page.LastUpdated = DateTime.UtcNow;
                page.LastUpdatedBy = "seed";
            }
            else
            {
                page = new Page
                {
                    PageId = Guid.NewGuid(),
                    Title = Title,
                    HeroImage = HeroImage,
                    HtmlContent = HtmlContent,
                    OrderRanking = OrderRanking,
                    PageIcon = PageIcon,
                    CreatedBy = "seed",
                    CreatedDate = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow,
                    ShowPageDetails = ShowPageDetails,
                    LastUpdatedBy = "seed"
                };
                context.Pages.Add(page);
            }

            var createupdatePage = context.SaveChanges();
        }
        // ----- This method populates DB with default Blog
        private void SeedBlog(MyBlogSiteDatabaseContext context)
        {
            var blog = context.Blogs.FirstOrDefault();
            if (blog == null)
            {
                var cat = context.BlogCategories.FirstOrDefault(u => u.Name == "General");
                var su = context.Users.FirstOrDefault(u => u.UserName == "admin@myblogsite.com");
                blog = new Blog
                {
                    Article = "Donec eget ex magna. Interdum et malesuada fames ac ante ipsum primis in faucibus. Pellentesque venenatis dolor imperdiet dolor mattis sagittis. Praesent rutrum sem diam, vitae egestas enim auctor sit amet. Pellentesque leo mauris, consectetur id ipsum sit amet, fergiat. Pellentesque in mi eu massa lacinia malesuada et a elit. Donec urna ex, lacinia in purus ac, pretium pulvinar mauris. Curabitur sapien risus, commodo eget turpis at, elementum convallis elit. Pellentesque enim turpis, hendrerit tristique.",
                    CreatedBy = su.Id,
                    HeroImage = "/content/images/pic13.jpg",
                    CreatedDate = DateTime.Now,
                    PublishDate = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    LastUpdatedBy = su.Id,
                    Title = "Default Blog",
                    BlogCategoryId = cat.BlogCategoryId
                };
                context.Blogs.Add(blog);
                var createupdateBlog = context.SaveChanges();
                blog = context.Blogs.FirstOrDefault();
                blog.CreatedBy = su.Id;
                context.SaveChanges();
            }
        }
    }
}