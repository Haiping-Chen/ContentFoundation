using ContentFoundation.Core;
using ContentFoundation.Core.Account;
using CustomEntityFoundation;
using EntityFrameworkCore.BootKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ContentFoundation.RestApi
{
    [Authorize]
    [Produces("application/json")]
    [Route("cf/[controller]")]
    public class CoreController : ControllerBase
    {
        protected Database dc { get; set; }

        public CoreController()
        {
            dc = new Database();

            string db = CefOptions.Configuration.GetSection("Database:Default").Value;
            string connectionString = CefOptions.Configuration.GetSection("Database:ConnectionStrings")[db];

            if (db.Equals("SqlServer"))
            {
                dc.BindDbContext<IDbRecord, DbContext4SqlServer>(new DatabaseBind
                {
                    MasterConnection = new SqlConnection(connectionString),
                    CreateDbIfNotExist = true,
                    AssemblyNames = CefOptions.Assembles
                });
            }
            else if (db.Equals("Sqlite"))
            {
                connectionString = connectionString.Replace("|DataDirectory|\\", CefOptions.ContentRootPath + "\\App_Data\\");
                dc.BindDbContext<IDbRecord, DbContext4Sqlite>(new DatabaseBind
                {
                    MasterConnection = new SqliteConnection(connectionString),
                    CreateDbIfNotExist = true,
                    AssemblyNames = CefOptions.Assembles
                });
            }
            else if (db.Equals("MySql"))
            {
                dc.BindDbContext<IDbRecord, DbContext4MySql>(new DatabaseBind
                {
                    MasterConnection = new MySqlConnection(connectionString),
                    CreateDbIfNotExist = true,
                    AssemblyNames = CefOptions.Assembles
                });
            }
            else if (db.Equals("InMemory"))
            {
                dc.BindDbContext<IDbRecord, DbContext4Memory>(new DatabaseBind
                {
                    AssemblyNames = CefOptions.Assembles
                });
            }
        }

        protected String GetConfig(string path)
        {
            if (String.IsNullOrEmpty(path)) return String.Empty;
            
            return CefOptions.Configuration.GetSection(path).Value;
        }

        protected List<KeyValuePair<String, String>> GetSection(string path)
        {
            return CefOptions.Configuration.GetSection(path).AsEnumerable().ToList();
        }

        protected User GetCurrentUser()
        {
            if (this.User != null)
            {
                return new User
                {
                    Id = this.User.Claims.FirstOrDefault(x => x.Type.Equals("UserId"))?.Value,
                    Name = this.User.Claims.FirstOrDefault(x => x.Type.Equals("UserName"))?.Value
                };
            }
            else
            {
                return new User
                {
                    Id = Guid.Empty.ToString(),
                    Name = "Anonymous"
                };
            }
        }
    }
}
