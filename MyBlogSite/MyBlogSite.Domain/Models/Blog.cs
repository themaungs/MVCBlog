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
    public class Blog: AuditModelBase
    {
        public Guid BlogId { get; set; }
        [Required]
        public string Title { get; set; }
        [DisplayName("Hero Image")]
        public string HeroImage { get; set; }
        [AllowHtml]
        public string Article { get; set;}
        [Required]
        [DisplayName("Publish Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [UIHint("DateTimePicker")]
        public DateTime PublishDate { get; set; }
        [Required]
        public Guid BlogCategoryId { get; set; }
        public virtual BlogCategory BlogCategory { get; set; }

    }
}
