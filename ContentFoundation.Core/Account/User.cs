using CustomEntityFoundation.Bundles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using ContentFoundation.Core.Permission;
using EntityFrameworkCore.BootKit;

namespace ContentFoundation.Core.Account
{
    public class User : BundleDbRecord, IDbRecord
    {
        [Required]
        [StringLength(128)]
        public String Password { get; set; }

        [Required]
        [StringLength(64)]
        public String Email { get; set; }

        [Required]
        [StringLength(32)]
        public String FirstName { get; set; }

        [Required]
        [StringLength(32)]
        public String LastName { get; set; }

        [MaxLength(256)]
        public String Description { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime SignupDate { get; set; }

        public String FullName { get { return $"{FirstName} {LastName}"; } }

        public List<RoleOfUser> Roles { get; set; }

        public User()
        {
            SignupDate = DateTime.UtcNow;
        }
    }
}
