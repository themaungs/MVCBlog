using MyBlogSite.Domain.Models;
using MyBlogSite.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogSite.Domain.Interfaces
{
    public interface ICountryRepository : IBaseEntityFrameworkRepository<Country>
    {
        int GetCountryIdByName(string name);

        IEnumerable<Country> GetAll();
    }
}
