using MyBlogSite.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogSite.Domain.Interfaces
{
    public interface IPageRepository : IBaseEntityFrameworkRepository<Page>
    {
        Guid GetPageGuidByTitle(string title);
        Page GetPageByTitle(string title);
        Page GetPageByGuid(string guid);
        IEnumerable<Page> GetAll();
    }
}
