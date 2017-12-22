using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json.Linq;
using CustomEntityFoundation;
using CustomEntityFoundation.Entities;
using CustomEntityFoundation.Bundles;
using ContentFoundation.Core.Permission;
using EntityFrameworkCore.BootKit;

namespace ContentFoundation.Core.Menus
{
    public class Menu : BundleDbRecord, IDbRecord, IContentPermission
    {
        [MaxLength(256)]
        public string Description { get; set; }

        [MaxLength(32)]
        public string Icon { get; set; }

        [MaxLength(128)]
        public String Link { get; set; }

        [StringLength(36)]
        public String ParentId { get; set; }

        [StringLength(36)]
        public String PageId { get; set; }

        /// <summary>
        /// Menu sort
        /// </summary>
        public Int32 Priority { get; set; }

        public Boolean Hidden { get; set; }

        /// <summary>
        /// HTML href.target: _blank, _top
        /// </summary>
        [MaxLength(10)]
        public String Target { get; set; }

        [NotMapped]
        public List<Menu> Items { get; set; }

        public List<MenuAcl> Roles { get; set; }

        public IQueryable<IDbRecord> GetResources(EntityDbContext dc)
        {
            return dc.Table<Menu>();
        }

        public string GetResourceName()
        {
            return Name;
        }

        public string GetResourceDescription()
        {
            return Description;
        }
    }

    public class MenuAcl : Entity, IDbRecord
    {
        [StringLength(36)]
        public String MenuId { get; set; }

        public Menu Menu { get; set; }

        [StringLength(36)]
        public String RoleId { get; set; }

        public Role Role { get; set; }
    }
}
