using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ContentFoundation.Core.Account;
using System.Linq.Dynamic.Core;
using Quickflow.Core;
using CustomEntityFoundation.Utilities;
using CustomEntityFoundation.Entities;
using CustomEntityFoundation;
using EntityFrameworkCore.BootKit;
using Newtonsoft.Json.Linq;
using DotNetToolkit;

namespace ContentFoundation.RestApi.Account
{
    public class AccountController : CoreController
    {
        /// <summary>
        /// Get a valid token after login
        /// </summary>
        /// <param name="username">User Email</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("token")]
        [ProducesResponseType(typeof(String), 200)]
        public IActionResult Token(string username, string password)
        {
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                return new BadRequestObjectResult("Username and password should not be empty.");
            }

            // validate from local
            var user = dc.Table<User>().FirstOrDefault(x => x.Name == username && x.Password == password);
            if (user != null)
            {
                return Ok(JwtToken.GenerateToken(user));
            }

            return new BadRequestObjectResult($"Authorization Failed.");
        }

        /*[HttpGet("list")]
        public PageResult<UserListViewModel> GetUsers(string name, [FromQuery] int page = 1)
        {
            var query = dc.Table<User>().AsQueryable().Select(x => new UserListViewModel
            {
                Email = x.Email,
                Name = x.Name
            });

            if (!String.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }

            var total = query.Count();
            int size = 20;
            var items = query.Skip((page - 1) * size).Take(size).ToList();

            return new PageResult<UserListViewModel> { Total = total, Page = page, Size = size, Items = items };
        }*/

        /// <summary>
        /// Get current user profile
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public UserViewModel GetUser()
        {
            var user = dc.Table<User>().Find(GetCurrentUser().Id);
            return user.ToObject<UserViewModel>();
        }

        /// <summary>
        /// Check if user already exists
        /// </summary>
        /// <param name="userName">Email</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("exist")]
        public Boolean UserExist([FromQuery] String userName)
        {
            var user = dc.Table<User>().Any(x => x.Name == userName);

            return user;
        }
        
        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="oldpassword"></param>
        /// <param name="newpassword"></param>
        /// <returns></returns>
        [HttpPut("changepassword")]
        public bool ChangePassword(string oldpassword, string newpassword)
        {
            dc.DbTran(() => {
                var user = dc.Table<User>().FirstOrDefault(x => x.Id == GetCurrentUser().Id && x.Password == oldpassword);
                user.Password = newpassword;
            });

            return true;
        }

        /// <summary>
        /// Update current user profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accountModel"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateUser([FromRoute] String id, User accountModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return CreatedAtAction("UpdateUser", new { id = accountModel.Id }, accountModel.Id);
        }

        /// <summary>
        /// Sign up a new account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserCreateViewModel account)
        {
            var user = new User
            {
                Password = account.Password,
                Email = account.Email,
                Name = account.Email,
                FirstName = account.Email.Split("@").First(),
                LastName = account.Email.Split("@").Last(),
                ActivationToken = Guid.NewGuid().ToString("N")
            };

            dc.DbTran(async delegate {
                if (dc.Table<User>().Any(x => x.Email == account.Email)) return;

                var wf = new WorkflowEngine
                {
                    WorkflowId = "87647613-df6e-435c-b13c-b0f42397cbc0",
                    TransactionId = Guid.NewGuid().ToString(),
                };

                await wf.Run(dc, user);
            });

            return Ok();
        }

        [HttpGet("activate/{token}")]
        [AllowAnonymous]
        public IActionResult ActivateAccount([FromRoute] String token)
        {
            dc.DbTran(() => {
                var user = dc.Table<User>().FirstOrDefault(x => x.ActivationToken == token);
                user.ActivationToken = String.Empty;
            });

            return Ok();
        }
    }
}