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
    public class BlockInPage : Entity, IDbRecord
    {
        [Required]
        [StringLength(36)]
        public string PageId { get; set; }

        [Required]
        [StringLength(36)]
        public string BlockId { get; set; }

        public String PositionJson { get; set; }

        [NotMapped]
        public BlockPositionInPage Position
        {
            get { return String.IsNullOrEmpty(PositionJson) ? null : JsonConvert.DeserializeObject<BlockPositionInPage>(PositionJson); }
            set { PositionJson = (value == null) ? null : JsonConvert.SerializeObject(value); }
        }

        public override bool IsExist<T>(EntityDbContext dc)
        {
            return dc.Table<BlockInPage>().Any(x => x.PageId == PageId && x.BlockId == BlockId);
        }
    }

    public class BlockPositionInPage
    {
        public int W { get; set; }
        public int H { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
