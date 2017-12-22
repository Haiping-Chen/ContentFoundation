using CustomEntityFoundation;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Builder
{
    public static class EntityDbContextBuilderExtensions
    {
        /// <summary>
        /// Use CustomEntityFoundation
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <param name="contentRootPath"></param>
        /// <param name="assembles"></param>
        public static void UseEntityDbContext(this IApplicationBuilder app, IConfiguration configuration, String contentRootPath, String[] assembles)
        {
            Console.WriteLine($"     *   *   ***     ");
            Console.WriteLine($"     *   *    *      ");
            Console.WriteLine($"     *****    *      ");
            Console.WriteLine($"     *   *    *      ");
            Console.WriteLine($"     *   *   ***  * * *");

            var db = configuration.GetSection("Database:Default").Value;
            EntityDbContext.Options = new DatabaseOptions
            {
                Database = db,
                ConnectionString = configuration.GetSection("Database:ConnectionStrings")[db],
                ContentRootPath = contentRootPath
            };
            EntityDbContext.Assembles = assembles;
            EntityDbContext.Configuration = configuration;
        }
    }
}
