using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NLayer.Domain.UserSystemModule.Aggregates.MenuAgg;
using NLayer.Domain.UserSystemModule.Aggregates.RoleAgg;
using NLayer.Infrastructure.Entity;

namespace NLayer.Domain.UserSystemModule.Aggregates.UserAgg
{
    [Table("auth.User")]
    public class User : EntityBase
    {
        public override Guid Id { get; set; }

        public string Name { get; set; }

        public string LoginName { get; set; }

        public string LoginPwd { get; set; }

        public string Email { get; set; }

        public DateTime Created { get; set; }

        public string LastLoginToken { get; set; }

        public DateTime LastLogin { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }

    }
}
