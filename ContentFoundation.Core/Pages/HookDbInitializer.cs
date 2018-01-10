using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CustomEntityFoundation;
using EntityFrameworkCore.BootKit;

namespace ContentFoundation.Core.Pages
{
    public class HookDbInitializer : IHookDbInitializer
    {
        public int Priority => 100;

        public void Load(IConfiguration config, Database dc)
        {
            Directory.GetFiles(Database.ContentRootPath + "\\App_Data\\DbInitializer", "*.Pages.json")
                .ToList()
                .ForEach(path =>
                {
                    string json = File.ReadAllText(path);
                    var dbContent = JsonConvert.DeserializeObject<JObject>(json);

                    if(dbContent["pages"] != null)
                    {
                        InitPages(dc, dbContent["pages"].ToList());
                    }

                });
        }

        private void InitPages(Database dc, List<JToken> jPages)
        {
            jPages.ForEach(jPage => {
                var dmPage = jPage.ToObject<Page>();
                if (!dmPage.IsExist<Page>(dc))
                {
                    dmPage.Add(dc);
                }
            });
        }
    }
}
