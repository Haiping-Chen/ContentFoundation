using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CustomEntityFoundation;
using EntityFrameworkCore.BootKit;

namespace ContentFoundation.Core.Menus
{
    public class HookDbInitializer : IHookDbInitializer
    {
        public int Priority => 100;

        public void Load(IConfiguration config, Database dc)
        {
            Directory.GetFiles(CefOptions.ContentRootPath + "\\App_Data\\DbInitializer", "*.Menus.json")
                .ToList()
                .ForEach(path =>
                {
                    string json = File.ReadAllText(path);
                    var dbContent = JsonConvert.DeserializeObject<JObject>(json);

                    if(dbContent["menus"] != null)
                    {
                        SaveMenu(dc, dbContent["menus"].ToList());
                    }

                });
        }

        private void SaveMenu(Database dc, List<JToken> menus)
        {
            menus.ForEach(jMenu => {

                var dm = jMenu.ToObject<Menu>();
                if (!dm.IsExist<Menu>(dc))
                {
                    dc.Table<Menu>().Add(dm);

                    if (dm.Items != null)
                    {
                        dm.Items.ForEach(subMenu => {

                            subMenu.BundleId = dm.BundleId;
                            subMenu.ParentId = dm.Id;

                            dc.Table<Menu>().Add(subMenu);
                        });
                    }
                }

            });
        }
    }
}
