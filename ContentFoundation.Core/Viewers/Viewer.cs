using CustomEntityFoundation.Entities;
using EntityFrameworkCore.BootKit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ContentFoundation.Core.Viewers
{
    /// <summary>
    /// Present data from view
    /// </summary>
    public class Viewer : Entity, IDbRecord
    {
        [Required]
        [MaxLength(64)]
        public String Name { get; set; }

        [Required]
        [StringLength(36)]
        public String ViewId { get; set; }

        /// <summary>
        /// View Mode: Default is Table
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String Presenter { get; set; }

        [ForeignKey("ViewerId")]
        public List<ActionInViewer> Actions { get; set; }
    }
}
