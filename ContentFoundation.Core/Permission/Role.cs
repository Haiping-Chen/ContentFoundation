using CustomEntityFoundation.Entities;
using EntityFrameworkCore.BootKit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ContentFoundation.Core.Permission
{
    /// <summary>
    /// User Role
    /// </summary>
    public class Role : Entity, IDbRecord
    {
        [Required]
        [StringLength(36)]
        public String Name { get; set; }

        [MaxLength(128)]
        public String Description { get; set; }
    }
}
