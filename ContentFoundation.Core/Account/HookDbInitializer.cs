using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.IO;
using Newtonsoft.Json.Linq;
using CustomEntityFoundation;

namespace ContentFoundation.Core.Account
{
    public class HookDbInitializer : IHookDbInitializer
    {
        public int Priority => 11;

        public void Load(IConfiguration config, EntityDbContext dc)
        {
            Directory.GetFiles(EntityDbContext.Options.ContentRootPath + "\\App_Data\\DbInitializer", "*.Accounts.json")
                .ToList()
                .ForEach(path =>
                {
                    string json = File.ReadAllText(path);
                    var dbContent = JsonConvert.DeserializeObject<JObject>(json);

                    if (dbContent["accounts"] != null)
                    {
                        InitAccounts(dc, dbContent["accounts"].ToList());
                    }
                });
        }

        private void InitAccounts(EntityDbContext dc, List<JToken> jUsers)
        {
            jUsers.ForEach(jUser => {
                var user = jUser.ToObject<User>();
                if (!user.IsExist<User>(dc))
                {
                    dc.Table<User>().Add(user);
                }
            });
        }
    }
}
