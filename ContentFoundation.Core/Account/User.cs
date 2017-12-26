using CustomEntityFoundation.Bundles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using ContentFoundation.Core.Permission;
using EntityFrameworkCore.BootKit;
using Newtonsoft.Json.Serialization;
using CustomEntityFoundation.Fields;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContentFoundation.Core.Account
{
    public class User : BundleDbRecord, IDbRecord
    {
        [Required]
        [StringLength(128)]
        [DataType(DataType.Password)]
        [EntityPropertyAsField("Password", "Text")]
        public String Password { get; set; }

        [Required]
        [StringLength(64)]
        [DataType(DataType.EmailAddress)]
        [EntityPropertyAsField("Email", "Text")]
        public String Email { get; set; }

        [Required]
        [StringLength(32)]
        [EntityPropertyAsField("FirstName", "Text")]
        public String FirstName { get; set; }

        [Required]
        [StringLength(32)]
        [EntityPropertyAsField("LastName", "Text")]
        public String LastName { get; set; }

        [MaxLength(256)]
        [EntityPropertyAsField("Description", "Text")]
        public String Description { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime SignupDate { get; set; }

        [NotMapped]
        public String ActivationToken { get; set; }

        public String FullName { get { return $"{FirstName} {LastName}"; } }

        public List<RoleOfUser> Roles { get; set; }

        public User()
        {
            SignupDate = DateTime.UtcNow;
        }
    }
}
