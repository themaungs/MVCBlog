﻿using MyBlogSite.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogSite.Domain.Interfaces
{
    public interface IBlogCategoryRepository : IBaseEntityFrameworkRepository<BlogCategory>
    {
        IEnumerable<BlogCategory> GetAll();
    }
}