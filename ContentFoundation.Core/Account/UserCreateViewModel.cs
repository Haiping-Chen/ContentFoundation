using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ContentFoundation.Core.Account
{
    public class UserCreateViewModel
    {
        [Required]
        public String Email { get; set; }

        [Required]
        public String Password { get; set; }
    }
}
