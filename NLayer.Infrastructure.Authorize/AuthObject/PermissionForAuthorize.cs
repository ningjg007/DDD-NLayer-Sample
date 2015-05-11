using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Infrastructure.Authorize.AuthObject
{
    public class PermissionForAuthorize
    {
        public Guid PermissionId { get; set; }

        public String PermissionCode { get; set; }

        public Guid MenuId { get; set; }

        public Guid RoleId { get; set; }

        public bool FromUser { get; set; }
    }
}
