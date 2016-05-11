using MyBlogSite.Domain.Interfaces;
using MyBlogSite.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogSite.Domain.Repositories
{
    public class UserRepository : BaseEntityFrameworkRepository<MyBlogSiteUser>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public MyBlogSiteUser GetUserById(string UserId)
        {
            return Context.Users.Where(u => u.Id == UserId).FirstOrDefault();
        }

        public MyBlogSiteUser GetUserByUserName(string UserName)
        {
            return Context.Users.Where(u => u.UserName == UserName).FirstOrDefault();
        }
        public IEnumerable<MyBlogSiteUser> GetAll()
        {
            return Context.Users.ToList();
        }

    }
}
