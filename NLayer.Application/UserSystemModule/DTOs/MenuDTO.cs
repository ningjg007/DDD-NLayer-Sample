using System;
using System.Collections.Generic;

namespace NLayer.Application.UserSystemModule.DTOs
{
    public class MenuDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Url { get; set; }

        public string Module { get; set; }

        public int SortOrder { get; set; }

        public DateTime Created { get; set; }

        public virtual ICollection<PermissionDTO> Permissions { get; set; }
    }
}
