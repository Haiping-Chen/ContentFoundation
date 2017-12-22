using ContentFoundation.Core.Account;
using CustomEntityFoundation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentFoundation.RestApi
{
    [Authorize]
    [Produces("application/json")]
    [Route("cf/[controller]")]
    public class CoreController : ControllerBase
    {
        protected EntityDbContext dc { get; set; }

        public CoreController()
        {
            dc = new EntityDbContext();
            dc.InitDb();
        }

        protected String GetConfig(string path)
        {
            if (String.IsNullOrEmpty(path)) return String.Empty;
            
            return EntityDbContext.Configuration.GetSection(path).Value;
        }

        protected List<KeyValuePair<String, String>> GetSection(string path)
        {
            return EntityDbContext.Configuration.GetSection(path).AsEnumerable().ToList();
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
