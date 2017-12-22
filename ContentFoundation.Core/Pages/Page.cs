using ContentFoundation.Core.Blocks;
using CustomEntityFoundation;
using CustomEntityFoundation.Entities;
using EntityFrameworkCore.BootKit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ContentFoundation.Core.Pages
{
    public class Page : Entity, IDbRecord
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Entity Name cannot be longer than 50 characters.")]
        public String Name { get; set; }

        [MaxLength(128)]
        public String Description { get; set; }

        [MaxLength(64)]
        public String UrlPath { get; set; }

        [NotMapped]
        public List<Block> Blocks { get; set; }

        public override bool IsExist<T>(EntityDbContext dc)
        {
            return dc.Table<Page>().Any(x => x.Name == Name);
        }
    }
}
