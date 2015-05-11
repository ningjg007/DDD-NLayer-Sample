using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Infrastructure.Authorize.AuthObject
{
    public class UserForAuthorize
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string LoginName { get; set; }

        public List<MenuForAuthorize> Menus { get; set; }

        public List<PermissionForAuthorize> Permissions { get; set; }
    }
}
