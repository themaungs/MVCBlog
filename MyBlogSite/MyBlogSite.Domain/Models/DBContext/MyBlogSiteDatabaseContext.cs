using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MyBlogSite.Domain.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Validation;
using System.Threading.Tasks;

namespace MyBlogSite.Domain.Models.DBContext
{
    public class MyBlogSiteDatabaseContext : IdentityDbContext<MyBlogSiteUser>, IUnitOfWork
    {
        public MyBlogSiteDatabaseContext() :  base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }
        public virtual IDbSet<Country> Countries { get; set; }
        public virtual IDbSet<BlogCategory> BlogCategories { get; set; }
        public virtual IDbSet<Blog> Blogs { get; set; }
        public virtual IDbSet<BlogTag> BlogTags { get; set; }
        public virtual IDbSet<Page> Pages { get; set; }
        public virtual IDbSet<PageDetail> PageDetails { get; set; }
        public override int SaveChanges()
        {
            var currentTime = DateTime.Now;
            MyBlogSiteDatabaseContext dbContext = new MyBlogSiteDatabaseContext();

            var currentUserName = "";
            currentUserName = HttpContext.Current == null ? "" : HttpContext.Current.User.Identity.Name;
            var userIsAuthenticated = currentUserName == "" ? false : true;
            var currentUser = dbContext.Users.Where(u => u.UserName == currentUserName).Select(u => u.Id).FirstOrDefault();
            
            currentUser = userIsAuthenticated ? currentUser == null ? "non-HttpContext user" : currentUser : "non-HttpContext user";
            foreach (var entry in
                ChangeTracker.Entries().
                    Where(
                        e =>
                            (e.State == EntityState.Added || e.State == EntityState.Modified ||
                             e.State == EntityState.Deleted)
                            && e.Entity is IAuditModelBase))
            {
                if (entry.State != EntityState.Deleted)
                {
                    var auditableEntity = entry.Entity as IAuditModelBase;
                    if (auditableEntity != null)
                    {
                        if (entry.State == EntityState.Added)
                        {
                            auditableEntity.CreatedDate = currentTime;
                            auditableEntity.CreatedBy = currentUser;
                        }
                        auditableEntity.LastUpdated = currentTime;
                        auditableEntity.LastUpdatedBy = currentUser;
                    }
                }
            }
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            var currentTime = DateTime.Now;
            MyBlogSiteDatabaseContext dbContext = new MyBlogSiteDatabaseContext();
            var currentUserName = "";
            currentUserName = HttpContext.Current == null ? "" : HttpContext.Current.User.Identity.Name;
            var userIsAuthenticated = currentUserName==""? false :true;
            var currentUser = dbContext.Users.Where(u => u.UserName == currentUserName).Select(u => u.Id).FirstOrDefault();
            currentUser = userIsAuthenticated ? currentUser : "non-HttpContext user";
            foreach (var entry in
                ChangeTracker.Entries().
                    Where(
                        e =>
                            (e.State == EntityState.Added || e.State == EntityState.Modified ||
                             e.State == EntityState.Deleted)
                            && e.Entity is IAuditModelBase))
            {
                if (entry.State != EntityState.Deleted)
                {
                    var auditableEntity = entry.Entity as IAuditModelBase;
                    if (auditableEntity != null)
                    {
                        if (entry.State == EntityState.Added)
                        {
                            auditableEntity.CreatedDate = currentTime;
                            auditableEntity.CreatedBy = currentUser;
                        }
                        auditableEntity.LastUpdated = currentTime;
                        auditableEntity.LastUpdatedBy = currentUser;
                    }
                }
            }
            return base.SaveChangesAsync();
        }

        public static MyBlogSiteDatabaseContext Create()
        {
            return new MyBlogSiteDatabaseContext();
        }

    }
}