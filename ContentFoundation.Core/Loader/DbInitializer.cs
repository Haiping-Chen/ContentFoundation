using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using CustomEntityFoundation.Utilities;
using CustomEntityFoundation;
using EntityFrameworkCore.BootKit;

namespace ContentFoundation.Core.Loader
{
    public class DbInitializer : IInitializationLoader
    {
        public int Priority => 1;

        public void Initialize()
        {
            var dc = new Database();
            dc.Init(CefOptions.Configuration, CefOptions.Assembles);
            

            var instances = TypeHelper.GetInstanceWithInterface<IHookDbInitializer>(CefOptions.Assembles).OrderBy(x => x.Priority).ToList();

            for (int idx = 0; idx < instances.Count; idx++)
            {
                DateTime start = DateTime.UtcNow;
                Console.WriteLine($"{instances[idx].ToString()} P:{instances[idx].Priority} started at {DateTime.UtcNow}");
                int effected = dc.DbTran(() => instances[idx].Load(CefOptions.Configuration, dc));
                Console.WriteLine($"{instances[idx].ToString()} effected [{effected}] records in {(DateTime.UtcNow - start).TotalMilliseconds} ms");
            }
        }
    }
}
