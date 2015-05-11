using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Application.Exceptions
{
    /// <summary>
    /// 登陆密码错误
    /// </summary>
    public class LoginPasswordIncorrectException : DefinedException
    {
        public LoginPasswordIncorrectException(string message)
            : base(message)
        {
            
        }
    }
}
