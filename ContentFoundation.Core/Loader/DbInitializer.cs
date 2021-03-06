﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using CustomEntityFoundation.Utilities;
using CustomEntityFoundation;
using EntityFrameworkCore.BootKit;
using DotNetToolkit;

namespace ContentFoundation.Core.Loader
{
    public class DbInitializer : IInitializationLoader
    {
        public int Priority => 1;

        public void Initialize()
        {
            var dc = new DefaultDataContextLoader().GetDefaultDc();

            var instances = TypeHelper.GetInstanceWithInterface<IHookDbInitializer>(Database.Assemblies).OrderBy(x => x.Priority).ToList();

            for (int idx = 0; idx < instances.Count; idx++)
            {
                DateTime start = DateTime.UtcNow;
                Console.WriteLine($"{instances[idx].ToString()} P:{instances[idx].Priority} started at {DateTime.UtcNow}");
                int effected = dc.DbTran(() => instances[idx].Load(Database.Configuration, dc));
                Console.WriteLine($"{instances[idx].ToString()} effected [{effected}] records in {(DateTime.UtcNow - start).TotalMilliseconds} ms");
            }
        }
    }
}
