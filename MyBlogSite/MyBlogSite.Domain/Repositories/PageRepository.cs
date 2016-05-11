using MyBlogSite.Domain.Interfaces;
using MyBlogSite.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlogSite.Domain.Repositories
{

    public class PageRepository : BaseEntityFrameworkRepository<Page>, IPageRepository
    {
        public PageRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public IEnumerable<Page> GetAll()
        {
            return Context.Pages.OrderBy(s => s.Title).ToList();
        }

        public Guid GetPageGuidByTitle(string title)
        {
            return Context.Pages.FirstOrDefault(s => s.Title == title).PageId;
        }
        public Page GetPageByTitle(string title)
        {
            return Context.Pages.FirstOrDefault(p => p.Title == title);
        }

        public Page GetPageByGuid(string guid)
        {
            return Context.Pages.FirstOrDefault(p => p.PageId.ToString() == guid);
        }
    }
}