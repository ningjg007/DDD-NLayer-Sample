using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Infrastructure.Authorize.AuthObject
{
    public class MenuForAuthorize
    {
        public int Module { get; set; }

        public Guid MenuId { get; set; }

        public string MenuName { get; set; }

        public string MenuUrl { get; set; }
    }
}
