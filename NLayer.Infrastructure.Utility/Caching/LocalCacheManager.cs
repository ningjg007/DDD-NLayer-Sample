using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace NLayer.Infrastructure.Utility.Caching
{
    public class LocalCacheManager : ICacheManager
    {
        protected Cache Cache
        {
            get
            {
                return HttpRuntime.Cache;
            }
        }

        public T Get<T>(string key)
        {
            return (T)Cache[key];
        }

        /*
            absoluteExpiration
            类型：System.DateTime
            所插入对象将过期并被从缓存中移除的时间。若要避免可能出现的本地时间方面的问题（如从标准时间更改为夏时制），请对此参数值使用 UtcNow，不要使用Now。如果使用绝对过期，则slidingExpiration 参数必须为 NoSlidingExpiration。
 
            slidingExpiration
            类型：System.TimeSpan
            最后一次访问所插入对象时与该对象过期时之间的时间间隔。如果该值等效于 20 分钟，则对象在最后一次被访问 20分钟之后将过期并被从缓存中移除。如果使用可调过期，则 absoluteExpiration 参数必须为NoAbsoluteExpiration。
         */

        public void Set(string key, object value, int minutes)
        {
            Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(minutes));
        }

        public void Set(string key, object value, int minutes, bool isAbsoluteExpiration, Action<string, object, string> onRemove)
        {
            SetCallBack(key, value, minutes, isAbsoluteExpiration, (k, v, reason) =>
            {
                if (onRemove != null)
                    onRemove(k, v, reason.ToString());
            });
        }

        private void SetCallBack(string name, object value, int minutes, bool isAbsoluteExpiration, CacheItemRemovedCallback onRemoveCallback)
        {
            if (isAbsoluteExpiration)
                Cache.Insert(name, value, null, DateTime.Now.AddMinutes(minutes), Cache.NoSlidingExpiration, CacheItemPriority.Normal, onRemoveCallback);
            else
                Cache.Insert(name, value, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(minutes), CacheItemPriority.Normal, onRemoveCallback);
        }

        public bool IsSet(string key)
        {
            return (Cache[key] != null);
        }

        public void Remove(string key)
        {
            if (Cache[key] != null)
                Cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var keys = new List<string>();
            var enumerator = Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var key = enumerator.Key.ToString();
                if (Regex.IsMatch(key, pattern, RegexOptions.IgnoreCase))
                    keys.Add(key);
            }

            foreach (var k in keys)
            {
                Cache.Remove(k);
            }
        }

        public void ClearAll()
        {
            var enumerator = Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var key = enumerator.Key.ToString();
                Cache.Remove(key);
            }
        }
    }
}
