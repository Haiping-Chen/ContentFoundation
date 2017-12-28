using ContentFoundation.Core.Pages;
using CustomEntityFoundation;
using CustomEntityFoundation.Entities;
using EntityFrameworkCore.BootKit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ContentFoundation.Core.Blocks
{
    public class Block : Entity, IDbRecord
    {
        [MaxLength(64)]
        public String Name { get; set; }

        [MaxLength(128)]
        public String Description { get; set; }

        [StringLength(36)]
        public String ViewId { get; set; }

        public int Priority { get; set; }

        [NotMapped]
        public List<KeyValuePair<String, String>> Menus { get; set; }

        [NotMapped]
        public BlockPositionInPage Position { get; set; }

        public override bool IsExist<T>(Database dc)
        {
            return dc.Table<Block>().Any(x => x.Name == Name);
        }
    }
}
