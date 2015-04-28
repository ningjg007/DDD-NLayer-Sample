using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Infrastructure.Helper
{
    public static class TextHelper
    {
        /// <summary>
        /// 检查字符串是不是为空/Null/空白字符
        /// </summary>
        /// <param name="self">要检查的字符串</param>
        /// <returns></returns>
        public static bool IsNullOrBlank(this string self)
        {
            return string.IsNullOrWhiteSpace(self);
        }

        public static bool NotNullOrBlank(this string self)
        {
            return !string.IsNullOrWhiteSpace(self);
        }

        public static bool EqualsIgnoreCase(this string self, string other)
        {
            return self.Equals(other, StringComparison.OrdinalIgnoreCase);
        }
    }
}
