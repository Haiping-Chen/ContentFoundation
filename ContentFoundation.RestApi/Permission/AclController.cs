using ContentFoundation.Core.Menus;
using ContentFoundation.Core.Permission;
using ContentFoundation.RestApi;
using CustomEntityFoundation.Entities;
using CustomEntityFoundation.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;

namespace ContentFoundation.RestApi.Permission
{
    public class AclController : CoreController
    {
        [HttpGet]
        public Dictionary<String, List<ResourceViewModel>> GetResources()
        {
            var list = TypeHelper.GetInstanceWithInterface<IContentPermission>("Core");

            Dictionary<String, List<ResourceViewModel>> result = new Dictionary<string, List<ResourceViewModel>>();

            /*list.ForEach(x => {
                String entityName = (x as IDbRecord).GetEntityName();
                result[entityName] = x.GetResources(dc).Select(r => new ResourceViewModel {
                    //Id = r.GetRecordId(),
                    Name = (r as ICorePermission).GetResourceName(),
                    Description = (r as ICorePermission).GetResourceDescription()
                }).ToList();
            });*/

            return result;
        }

        [HttpGet("myresources")]
        public IEnumerable<String> GetCurrentUserResources()
        {
            var resources = from acl in dc.Table<MenuAcl>()
                            join ur in dc.Table<RoleOfUser>() on acl.RoleId equals ur.RoleId
                            where ur.UserId == GetCurrentUser().Id
                            select acl.MenuId;

            return resources;
        }

        [HttpGet("{userId}")]
        public IEnumerable<String> GetCurrentUserResources([FromRoute] String userId)
        {
            var resources = from acl in dc.Table<MenuAcl>()
                            join ur in dc.Table<RoleOfUser>() on acl.RoleId equals ur.RoleId
                            where ur.UserId == userId
                            select acl.MenuId;

            return resources;
        }
    }

    public class ResourceViewModel
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
    }
}
