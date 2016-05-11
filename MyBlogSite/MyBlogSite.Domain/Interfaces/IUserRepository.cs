using MyBlogSite.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogSite.Domain.Interfaces
{
    public interface IUserRepository: IBaseEntityFrameworkRepository<MyBlogSiteUser>
    {
        MyBlogSiteUser GetUserById(string UserId);
        MyBlogSiteUser GetUserByUserName(string UserName);
        IEnumerable<MyBlogSiteUser> GetAll();
    }
}
