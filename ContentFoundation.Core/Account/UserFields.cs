using CustomEntityFoundation.Entities;
using CustomEntityFoundation.Fields;
using EntityFrameworkCore.BootKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContentFoundation.Core.Account
{
    public class UserTextField : TextField, IDbRecord { }
    public class UserEntityReferenceField : EntityReferenceField, IDbRecord { }
}
