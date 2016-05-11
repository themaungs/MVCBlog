using MyBlogSite.Domain.Interfaces;
using MyBlogSite.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogSite.Domain.Repositories
{
    public class BlogCategoryRepository : BaseEntityFrameworkRepository<BlogCategory>, IBlogCategoryRepository
    {
        public BlogCategoryRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public IEnumerable<BlogCategory> GetAll()
        {
            return Context.BlogCategories;
        }
    }
}
