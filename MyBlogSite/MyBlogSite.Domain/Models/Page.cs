using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyBlogSite.Domain.Models
{
    public class Page:AuditModelBase
    {
        public Guid PageId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [DisplayName("Page Icon")]
        public string PageIcon { get; set; }
        [DisplayName("Hero Image")]
        public string HeroImage { get; set; }
        [DisplayName("Sub Header")]
        public string Title2 { get; set; }
        [Required]
        public int OrderRanking { get; set; }
        [AllowHtml]
        public string HtmlContent { get; set; }
        [Required]
        [DisplayName("Show Page Details")]
        public bool ShowPageDetails { get; set; }
    }
}
