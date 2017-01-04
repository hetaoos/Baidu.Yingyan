using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baidu.Yingyan.Converters
{
    public class UnixTicksConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var t = serializer.Deserialize<long?>(reader);
            if (t != null)
                return t.Value.FromUtcTicks();
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null && value is DateTime)
            {
                var p = (long)((DateTime)value).ToUtcTicks();
                serializer.Serialize(writer, p);
            }
        }
    }
}
