using MyBlogSite.Domain.Interfaces;
using MyBlogSite.Domain.Models;
using MyBlogSite.Domain.Interfaces;
using MyBlogSite.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogSite.Domain.Repositories
{
    public class CountryRepository : BaseEntityFrameworkRepository<Country>, ICountryRepository
    {
        public CountryRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public int GetCountryIdByName(string name)
        {
            var query = Context.Countries.Where(c => c.Name == name);
            var country = query.FirstOrDefault();
            return country == null ? 0 : country.Id;
        }


        public IEnumerable<Country> GetAll()
        {
            return Context.Countries.OrderBy(s => s.Name).ToList();
        }
    }
}
