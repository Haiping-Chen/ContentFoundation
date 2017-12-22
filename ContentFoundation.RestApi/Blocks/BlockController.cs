using ContentFoundation.Core.Blocks;
using ContentFoundation.RestApi;
using CustomEntityFoundation.Models;
using CustomEntityFoundation.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentFoundation.RestApi.Blocks
{
    public class BlockController : CoreController
    {
        [HttpGet("Query")]
        public PageResult<Block> GetBlocks(string name, [FromQuery] int page = 1)
        {
            var query = dc.Table<Block>().AsQueryable();
            if (!String.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }

            return new PageResult<Block>() { Page = page }.LoadData(query);
        }

        [HttpPost]
        public async Task<IActionResult> PostBlock(Block blockEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dc.DbTran(delegate
            {
                var dm = blockEntity.ToObject<Block>();
                dc.Table<Block>().Add(dm);
            });
            
            return CreatedAtAction("GetBlock", new { id = blockEntity.Id }, blockEntity.Id);
        }
    }
}
