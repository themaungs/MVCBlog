using MyBlogSite.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyBlogSite.ViewModels
{
    public class BlogVM
    {
        public Guid BlogId { get; set; }
        [Required]
        public string Title { get; set; }
        public string HeroImage { get; set; }
        [AllowHtml]
        public string Article { get; set; }
        [Required]
        [DisplayName("Publish Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [UIHint("DateTimePicker")]
        public DateTime PublishDate { get; set; }
        [Required]
        public Guid BlogCategoryId { get; set; }
        public virtual BlogCategory BlogCategory { get; set; }
        public string Blogger { get; set; }
        public string BlogCategoryName { get; set; }
        [StringLength(128)]
        [DisplayName("Created By")]
        public string CreatedBy { get; set; }

        [DisplayName("Creation Date")]
        public DateTime? CreatedDate { get; set; }

        [StringLength(128)]
        [DisplayName("Last Updated By")]
        public string LastUpdatedBy { get; set; }

        [DisplayName("Last Updated Date")]
        public DateTime? LastUpdated { get; set; }

    }
}
