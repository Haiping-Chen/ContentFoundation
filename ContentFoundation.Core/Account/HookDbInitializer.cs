using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.IO;
using Newtonsoft.Json.Linq;
using CustomEntityFoundation;
using EntityFrameworkCore.BootKit;

namespace ContentFoundation.Core.Account
{
    public class HookDbInitializer : IHookDbInitializer
    {
        public int Priority => 110;

        public void Load(IConfiguration config, Database dc)
        {
            Directory.GetFiles(CefOptions.ContentRootPath + "\\App_Data\\DbInitializer", "*.Accounts.json")
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

        private void InitAccounts(Database dc, List<JToken> jUsers)
        {
            jUsers.ForEach(jUser => {
                var user = jUser.ToObject<User>();
                if (!user.IsExist<User>(dc))
                {
                    user.Password = user.Name;
                    dc.Table<User>().Add(user);
                }
            });
        }
    }
}
