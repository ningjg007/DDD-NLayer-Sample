using System;
using NLayer.Application.Modules;

namespace NLayer.Application.UserSystemModule.DTOs
{
    public class PermissionForAuthDTO
    {
        public Guid PermissionId { get; set; }

        public string PermissionCode { get; set; }

        public Guid MenuId { get; set; }

        public Guid RoleId { get; set; }

        public bool FromUser { get; set; }

        public string PermissionName { get; set; }

        public string MenuName { get; set; }

        public string RoleName { get; set; }

        public int PermissionSortOrder { get; set; }

        public int MenuSortOrder { get; set; }

        public NLayerModulesType Module { get; set; }

        public string ModuleName { get; set; }
    }
}
