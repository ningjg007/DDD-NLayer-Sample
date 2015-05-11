using System;

namespace NLayer.Application.UserSystemModule.DTOs
{
    public class PermissionForAuthDTO
    {
        public Guid PermissionId { get; set; }

        public String PermissionCode { get; set; }

        public Guid MenuId { get; set; }

        public Guid RoleId { get; set; }

        public bool FromUser { get; set; }
    }
}
