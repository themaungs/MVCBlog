using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyBlogSite.Domain.Interfaces
{
    public interface IAuditModelBase
    {
        [StringLength(128)]
        string CreatedBy { get; set; }
        DateTime? CreatedDate { get; set; }
        [StringLength(128)]
        string LastUpdatedBy { get; set; }
        DateTime? LastUpdated { get; set; }
        [Timestamp]
        byte[] RowVersion { get; set; }

    }
}
