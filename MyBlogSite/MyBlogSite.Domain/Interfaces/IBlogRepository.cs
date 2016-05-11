using MyBlogSite.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogSite.Domain.Interfaces
{
    public interface IBlogRepository : IBaseEntityFrameworkRepository<Blog>
    {
        Blog GetBlogByPublishDateTitle(DateTime date, string title);
        Blog GetBlogByGuid(string guid);
        IEnumerable<Blog> GetAll(bool isLoggedIn);
    }
}
