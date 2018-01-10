using ContentFoundation.Core.Permission;
using DotNetToolkit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ContentFoundation.RestApi.Permission
{
    public class RoleController : CoreController
    {
        [HttpGet]
        public PageResult<Role> GetRoles(string name, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var query = dc.Table<Role>().AsQueryable();
            if (!String.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }

            return new PageResult<Role> { Page = page, Size = size }.LoadRecords(query);
        }
    }
}
