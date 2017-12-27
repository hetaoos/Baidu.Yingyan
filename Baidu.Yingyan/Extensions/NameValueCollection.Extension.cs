using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Baidu.Yingyan
{
    /// <summary>
    /// NameValueCollectionExtension
    /// </summary>
    public static class NameValueCollectionExtension
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nameValuePairs">The name value pairs.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static T GetValue<T>(this NameValueCollection nameValuePairs, string key, T defaultValue)
            where T : IConvertible
        {
            if (nameValuePairs.AllKeys.Contains(key))
            {
                string tmpValue = nameValuePairs[key];
                try
                {
                    return (T)Convert.ChangeType(tmpValue, typeof(T));
                }
                catch { }
            }
            return defaultValue;
        }

        /// <summary>
        /// To the URI query.
        /// </summary>
        /// <param name="nameValuePairs">The name value pairs.</param>
        /// <returns></returns>
        public static string ToUriQuery(this NameValueCollection nameValuePairs)
        {
            return string.Join("&",
                     nameValuePairs.AllKeys
                         .Select(
                             key => string.Join("&", nameValuePairs.GetValues(key).Select(val => string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(val ?? string.Empty))))));
        }
    }
}