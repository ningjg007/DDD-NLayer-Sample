using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Application.Exceptions
{
    /// <summary>
    /// 登陆名不存在
    /// </summary>
    public class LoginNameNotFoundException : DefinedException
    {
        public LoginNameNotFoundException(string message)
            : base(message)
        {
            
        }
    }
}
