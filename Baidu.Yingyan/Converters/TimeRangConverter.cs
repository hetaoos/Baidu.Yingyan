using io.nulldata.Baidu.Yingyan.Fence;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io.nulldata.Baidu.Yingyan.Converters
{
    public class TimeRangConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var t = serializer.Deserialize<string>(reader);
            if (t != null)
            {
                var arr = t.Split(';')
                    .Select(o => new TimeRang(o));
                if (objectType.GetInterfaces().Contains(typeof(IList)))
                    return arr.ToList();
                else
                    return arr.ToArray();

            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null)
            {
                string s = null;
                if (value is IEnumerable<TimeRang>)
                    s = string.Join(",", (value as IEnumerable<TimeRang>).Select(o => o.ToString()).ToArray());
                else if (value is TimeRang[])
                {
                    s = string.Join(",", (value as TimeRang[]).Select(o => o.ToString()).ToArray());
                }
                serializer.Serialize(writer, s);
            }
        }
    }
}
