using MyBlogSite.Domain.Interfaces;
using MyBlogSite.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlogSite.Domain.Repositories
{

    public class BlogRepository : BaseEntityFrameworkRepository<Blog>, IBlogRepository
    {
        public BlogRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public IEnumerable<Blog> GetAll(bool isLoggedIn)
        {
            return Context.Blogs.Where(d=>d.PublishDate<=(isLoggedIn?d.PublishDate: DateTime.Now)).OrderByDescending(s => s.PublishDate);
        }

        public Blog GetBlogByPublishDateTitle(DateTime date, string title)
        {
            return Context.Blogs.Where(b => b.Title.ToLower()==title.ToLower() && b.PublishDate.ToShortDateString() == date.ToShortDateString()).FirstOrDefault();
        }

        public Blog GetBlogByGuid(string guid)
        {
            return Context.Blogs.Where(b => b.BlogId.ToString() == guid).FirstOrDefault();
        }
    }
}