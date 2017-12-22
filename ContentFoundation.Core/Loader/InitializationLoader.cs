using CustomEntityFoundation;
using CustomEntityFoundation.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContentFoundation.Core.Loader
{
    public class InitializationLoader
    {
        public void Load()
        {
            DateTime startAll = DateTime.UtcNow;
            Console.WriteLine();
            Console.WriteLine($"*** *** *** *** InitializationLoader running *** *** *** ***");
            Console.WriteLine();

            var coreLoaders = TypeHelper.GetInstanceWithInterface<IInitializationLoader>(EntityDbContext.Assembles);

            coreLoaders.ForEach(loader =>
            {
                DateTime start = DateTime.UtcNow;
                Console.WriteLine($"{loader.ToString()} P:{loader.Priority}...");
                loader.Initialize();
                Console.WriteLine($"{loader.ToString()} completed in {(DateTime.UtcNow - start).TotalSeconds} s.");
                Console.WriteLine();
            });

            Console.WriteLine($"*** *** *** *** InitializationLoader completed in {(DateTime.UtcNow - startAll).TotalSeconds} s. *** *** *** ***");
            Console.WriteLine();
        }
    }
}
