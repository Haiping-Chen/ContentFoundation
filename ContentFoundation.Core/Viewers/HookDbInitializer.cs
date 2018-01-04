using CustomEntityFoundation;
using EntityFrameworkCore.BootKit;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContentFoundation.Core.Viewers
{
    public class HookDbInitializer : IHookDbInitializer
    {
        public int Priority => 1010;

        public void Load(IConfiguration config, Database dc)
        {
            
        }
    }
}
