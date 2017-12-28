using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.IO;
using CustomEntityFoundation;
using EntityFrameworkCore.BootKit;

namespace ContentFoundation.Core.Permission
{
    public class HookDbInitializer : IHookDbInitializer
    {
        public int Priority => 100;

        public void Load(IConfiguration config, Database dc)
        {
            Directory.GetFiles(CefOptions.ContentRootPath + "\\App_Data\\DbInitializer", "*.Roles.json")
                .ToList()
                .ForEach(path =>
                {
                    string json = File.ReadAllText(path);
                    var dbContent = JsonConvert.DeserializeObject<JObject>(json);

                    if (dbContent["roles"] != null)
                    {
                        InitRoles(dc, dbContent["roles"].ToList());
                    }
                });
        }

        private void InitRoles(Database dc, List<JToken> roles)
        {
            roles.ForEach(jRole =>
            {
                var dm = jRole.ToObject<Role>();
                if (!dm.IsExist<Role>(dc))
                {
                    dc.Table<Role>().Add(dm);
                }
            });
        }
    }
}
