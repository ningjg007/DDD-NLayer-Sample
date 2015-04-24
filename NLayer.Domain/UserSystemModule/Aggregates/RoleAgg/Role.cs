using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NLayer.Domain.UserSystemModule.Aggregates.MenuAgg;
using NLayer.Domain.UserSystemModule.Aggregates.RoleGroupAgg;
using NLayer.Domain.UserSystemModule.Aggregates.UserAgg;
using NLayer.Infrastructure.Entity;

namespace NLayer.Domain.UserSystemModule.Aggregates.RoleAgg
{
    [Table("auth.Role")]
    public class Role : EntityBase
    {
        public override Guid Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }

        public int SortOrder { get; set; }

        public DateTime Created { get; set; }

        public virtual RoleGroup RoleGroup { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
