using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Infrastructure.Authorize
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class PermissionAttribute : Attribute
    {
        public PermissionAttribute(string code)
        {
            Code = code;
        }

        public PermissionAttribute(string code, int sortOrder)
        {
            Code = code;
            SortOrder = sortOrder;
        }

        public string Code { get; private set; }

        public int SortOrder { get; private set; }
    }
}
