using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogSite.Domain.Models
{
    public class BlogTag: AuditModelBase
    {
        public Guid BlogTagId { get; set; }
        public string Name { get; set; }
        [Required]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
