using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NLayer.Domain.UserSystemModule.Aggregates.RoleAgg;
using NLayer.Domain.UserSystemModule.Aggregates.UserAgg;
using NLayer.Infrastructure.Entity;

namespace NLayer.Domain.UserSystemModule.Aggregates.RoleGroupAgg
{
    [Table("auth.RoleGroup")]
    public class RoleGroup : EntityBase
    {
        public override Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int SortOrder { get; set; }

        public DateTime Created { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
