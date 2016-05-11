using System;
using System.ComponentModel.DataAnnotations;
using MyBlogSite.Domain.Interfaces;
using System.ComponentModel;

namespace MyBlogSite.Domain.Models
{
    public class AuditModelBase : IAuditModelBase
    {
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

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}