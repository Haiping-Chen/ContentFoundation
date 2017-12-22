using ContentFoundation.Core.Pages;
using ContentFoundation.Core.Utility;
using ContentFoundation.RestApi;
using CustomEntityFoundation.Models;
using CustomEntityFoundation.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentFoundation.RestApi.Pages
{
    public class PageController : CoreController
    {
        [HttpGet("Query")]
        public PageResult<Page> GetPages(string name, [FromQuery] int page = 1)
        {
            var query = dc.Table<Page>().AsQueryable();
            if (!String.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }

            return new PageResult<Page>() { Page = page }.LoadData(query);
        }

        // GET: api/Page/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPage([FromRoute] string id)
        {
            var page = dc.Table<Page>().Find(id);
            page.Load(dc);

            return Ok(page);
        }

        [HttpPut("{pageId}")]
        public async Task<IActionResult> UpdatePage([FromRoute] string pageId, [FromBody] Page pageEntity)
        {
            dc.DbTran(delegate
            {
                pageEntity.Update(dc);
            });

            return CreatedAtAction("GetPage", new { id = pageEntity.Id }, pageEntity.Id);
        }

        [HttpPost]
        public async Task<IActionResult> PostPage(Page pageEntity)
        {
            dc.DbTran(delegate
            {
                pageEntity.Add(dc);
            });
            
            return CreatedAtAction("GetPage", new { id = pageEntity.Id }, pageEntity.Id);
        }
    }
}
