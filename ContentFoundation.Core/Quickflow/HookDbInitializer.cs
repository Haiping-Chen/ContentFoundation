using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Linq;
using Newtonsoft.Json;
using Quickflow.Core.Entities;
using CustomEntityFoundation;
using EntityFrameworkCore.BootKit;

namespace Quickflow.Core
{
    public class HookDbInitializer : IHookDbInitializer
    {
        public int Priority => 100;

        public void Load(IConfiguration config, Database dc)
        {
            Directory.GetFiles(Database.ContentRootPath + "\\App_Data\\DbInitializer", "*.Workflows.json")
                .ToList()
                .ForEach(path =>
                {
                    string json = File.ReadAllText(path);
                    var dbContent = JsonConvert.DeserializeObject<JObject>(json);

                    if (dbContent["workflows"] != null)
                    {
                        Quickflow.Core.Utilities.DataInitialization.InitWorkflows(dc, dbContent["workflows"].ToList());
                    }

                });
        }
    }
}
