using Baidu.Yingyan.Fence;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baidu.Yingyan.Converters
{
    public class TimeOfDayConverter : JsonConverter
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
                return TimeOfDay.Parse(t);

            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is TimeOfDay)
            {
                serializer.Serialize(writer, value.ToString());
            }
        }
    }
}
