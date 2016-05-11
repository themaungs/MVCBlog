using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyBlogSite.Domain.Models
{
    public class PageDetail : AuditModelBase
    {
        public Guid PageDetailId { get; set; }
        [AllowHtml]
        public string Detail{get;set;}
        public Guid PageId { get; set; }
        public virtual Page Page { get; set; }
}
}
