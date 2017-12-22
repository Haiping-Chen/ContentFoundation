using ContentFoundation.Core.Permission;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContentFoundation.RestApi.Permission
{
    public class UserRoleController : CoreController
    {
        [HttpPost]
        public bool AddRole([FromBody] RoleOfUser userRoleEntity)
        {
            dc.Table<RoleOfUser>().Add(userRoleEntity);
            return true;
        }

        [HttpDelete]
        public bool RemoveRole([FromBody] RoleOfUser userRoleEntity)
        {
            return dc.DbTran(delegate
            {
                var userRole = dc.Table<RoleOfUser>()
                    .First(x => x.UserId == userRoleEntity.UserId && x.RoleId == userRoleEntity.RoleId);

                dc.Table<RoleOfUser>().Remove(userRole);
            }) > 0;
        }
    }
}
