using MyBlogSite.Domain.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace MyBlogSite.Domain.Models
{
    public class MyBlogSiteUser: IdentityUser, IAuditModelBase
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        [Required]
        [StringLength(20)]
        public string State { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(128)]
        public string LastUpdatedBy { get; set; }

        public DateTime? LastUpdated { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
            UserManager<MyBlogSiteUser> manager,
            string authenticationType = DefaultAuthenticationTypes.ApplicationCookie)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
