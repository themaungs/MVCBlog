using MyBlogSite.Domain.Interfaces;
using MyBlogSite.Domain.Models.DBContext;
using MyBlogSite.Domain.Repositories;
using MyBlogSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogSite.Helpers
{
    public class BlogHelper
    {

        private IBlogRepository _blogRepo;
        private IUserRepository _userRepo;
        private IBlogCategoryRepository _blogCatRepo;
        public BlogHelper()
        {
            _blogRepo = new BlogRepository(new MyBlogSiteDatabaseContext());
            _userRepo = new UserRepository(new MyBlogSiteDatabaseContext());
            _blogCatRepo = new BlogCategoryRepository(new MyBlogSiteDatabaseContext());
        }
        public IEnumerable<BlogVM> GetBlogs(bool isLoggedIn)
        {
            var blogs = _blogRepo.GetAll(isLoggedIn);
            var blogCat = _blogCatRepo.GetAll();
            var users = _userRepo.GetAll();
            var resultList = (from b in blogs
                              join u in users on b.CreatedBy equals u.Id
                              join c in blogCat on b.BlogCategoryId equals c.BlogCategoryId
                              where b.PublishDate <= (isLoggedIn ? b.PublishDate : DateTime.Now)
                              select new BlogVM
                              {
                                  HeroImage = b.HeroImage,
                                  Article = b.Article,
                                  BlogCategory = b.BlogCategory,
                                  BlogCategoryId = b.BlogCategoryId,
                                  BlogCategoryName = c.Name,
                                  Blogger = u.FirstName + ' ' + u.LastName,
                                  BlogId = b.BlogId,
                                  CreatedBy = b.CreatedBy,
                                  CreatedDate = b.CreatedDate,
                                  LastUpdated = b.LastUpdated,
                                  LastUpdatedBy = b.LastUpdatedBy,
                                  PublishDate = b.PublishDate,
                                  Title = b.Title
                              }
               ).OrderByDescending(s => s.PublishDate).ToList();
            return resultList;
        }
    }
}
