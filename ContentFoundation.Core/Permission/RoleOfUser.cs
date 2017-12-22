using ContentFoundation.Core.Account;
using CustomEntityFoundation.Entities;
using EntityFrameworkCore.BootKit;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContentFoundation.Core.Permission
{
    public class RoleOfUser : Entity, IDbRecord
    {
        [StringLength(36)]
        public String UserId { get; set; }

        public User User { get; set; }

        [StringLength(36)]
        public String RoleId { get; set; }

        public Role Role { get; set; }
    }
}
