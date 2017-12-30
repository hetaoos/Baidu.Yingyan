using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Baidu.Yingyan
{
    /// <summary>
    /// UriQueryExtension
    /// </summary>
    public static class UriQueryExtension
    {
        /// <summary>
        /// To the URI query.
        /// </summary>
        /// <param name="dic">The name value pairs.</param>
        /// <returns></returns>
        public static string ToUriQuery(this IDictionary<string, string> dic)
        {
            return string.Join("&", dic.Select(o => $"{WebUtility.UrlEncode(o.Key)}={WebUtility.UrlEncode(o.Value)}"));
        }
    }
}