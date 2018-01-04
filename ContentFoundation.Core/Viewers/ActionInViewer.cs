using CustomEntityFoundation.Entities;
using EntityFrameworkCore.BootKit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ContentFoundation.Core.Viewers
{
    public class ActionInViewer : Entity, IDbRecord
    {
        [Required]
        [MaxLength(64)]
        public String Name { get; set; }

        [MaxLength(256)]
        public String Description { get; set; }

        [Required]
        [StringLength(36)]
        public String ViewerId { get; set; }

        [Required]
        [MaxLength(128)]
        public String PostUrl { get; set; }
    }
}
