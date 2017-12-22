using CustomEntityFoundation;
using EntityFrameworkCore.BootKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContentFoundation.Core.Permission
{
    public interface IContentPermission
    {
        IQueryable<IDbRecord> GetResources(EntityDbContext dc);
        String GetResourceName();
        String GetResourceDescription();
    }
}
