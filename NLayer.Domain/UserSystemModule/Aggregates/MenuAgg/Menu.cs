using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NLayer.Infrastructure.Entity;

namespace NLayer.Domain.UserSystemModule.Aggregates.MenuAgg
{
    [Table("auth.Menu")]
    public class Menu : EntityBase
    {
        public override Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Url { get; set; }

        public string Module { get; set; }

        public int SortOrder { get; set; }

        public DateTime Created { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
