using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFrameworkCore.BootKit;
using ContentFoundation.Core.Permission;
using ContentFoundation.Core.Token;
using ContentFoundation.Core.Menus;

namespace ContentFoundation.RestApi.Menus
{
    public class MenuController : CoreController
    {
        [HttpGet]
        public async Task<IActionResult> GetMenus()
        {
            var menus = (from macl in dc.Table<MenuAcl>()
                        join m in dc.Table<Menu>() on macl.MenuId equals m.Id
                        join ur in dc.Table<RoleOfUser>() on macl.RoleId equals ur.RoleId
                        where !m.Hidden && ur.UserId == GetCurrentUser().Id
                        orderby m.Priority
                        select m).Distinct();

            string sql = menus.ToSql();
            var srvToken = new TokenService(GetSection("tokens"));

            // level 1
            var topMenus = menus.Where(x => String.IsNullOrEmpty(x.ParentId)).ToList();
            topMenus.ForEach(menu => {

                menu.Items = menus.Where(x => x.ParentId == menu.Id).ToList();

                menu.Items.ForEach(x => x.Link = srvToken.Replace(x.Link));
            });


            //TypeHelper.GetInstanceWithInterface<IHookMenu>("Core").ForEach(m => m.UpdateMenu(menu, dc));

            return Ok(topMenus);
        }

        
    }
}
