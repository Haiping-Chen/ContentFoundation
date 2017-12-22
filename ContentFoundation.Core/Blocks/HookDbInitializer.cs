using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CustomEntityFoundation;

namespace ContentFoundation.Core.Blocks
{
    public class HookDbInitializer : IHookDbInitializer
    {
        public int Priority => 100;

        public void Load(IConfiguration config, EntityDbContext dc)
        {
            Directory.GetFiles(EntityDbContext.Options.ContentRootPath + "\\App_Data\\DbInitializer", "*.Blocks.json")
                .ToList()
                .ForEach(path =>
                {
                    string json = File.ReadAllText(path);
                    var dbContent = JsonConvert.DeserializeObject<JObject>(json);

                    if(dbContent["blocks"] != null)
                    {
                        InitBlocks(dc, dbContent["blocks"].ToList());
                    }
                });
        }

        private void InitBlocks(EntityDbContext dc, List<JToken> jBlocks)
        {
            jBlocks.ForEach(jBlock => {
                var dmBlock = jBlock.ToObject<Block>();
                if (!dmBlock.IsExist<Block>(dc))
                {
                    dc.Table<Block>().Add(dmBlock);
                }
            });
        }
    }
}
