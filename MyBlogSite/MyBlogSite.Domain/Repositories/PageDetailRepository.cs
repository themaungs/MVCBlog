using MyBlogSite.Domain.Interfaces;
using MyBlogSite.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlogSite.Domain.Repositories
{

    public class PageDetailRepository : BaseEntityFrameworkRepository<PageDetail>, IPageDetailRepository
    {
        public PageDetailRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        public PageDetail GetPageDetailByGuid(string guid)
        {
            return Context.PageDetails.FirstOrDefault(p => p.PageDetailId.ToString() == guid);
        }
    }
}