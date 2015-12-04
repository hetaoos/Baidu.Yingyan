using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace System
{
    public static class NameValueCollectionExtension
    {
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

        public static string ToUriQuery(this NameValueCollection nameValuePairs)
        {
            return string.Join("&",
                     nameValuePairs.AllKeys
                         .Select(
                             key => string.Join("&", nameValuePairs.GetValues(key).Select(val => string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(val ?? string.Empty))))));
        }

    }
}
