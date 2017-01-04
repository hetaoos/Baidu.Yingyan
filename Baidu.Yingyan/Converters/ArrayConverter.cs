using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baidu.Yingyan.Converters
{
    public class ArrayConverter<T> : JsonConverter
    {
        private static Type type = typeof(T);
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var t = serializer.Deserialize<string>(reader);
            if (t != null)
            {
                var arr = t.Split(',').Select(o => (T)Convert.ChangeType(o, type));
                if (objectType.GetInterfaces().Contains(typeof(IList)))
                    return arr.ToList();
                else
                    return arr;

            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null)
            {
                string s = null;
                if (value is IEnumerable<T>)
                    s = string.Join(",", (value as IEnumerable<T>).Select(o => o.ToString()).ToArray());
                else if (value is string[])
                {
                    s = string.Join(",", (value as T[]).Select(o => o.ToString()).ToArray());
                }
                serializer.Serialize(writer, s);
            }
        }
    }
}
