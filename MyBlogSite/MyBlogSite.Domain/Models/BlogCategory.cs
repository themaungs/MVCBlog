using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogSite.Domain.Models
{
    public class BlogCategory: AuditModelBase
    {
        public Guid BlogCategoryId { get; set;}
        [Required]
        public string Name { get; set; }
    }
}
