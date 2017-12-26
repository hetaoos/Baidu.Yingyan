using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Baidu.Yingyan.Converters
{
    public class SimpleDateConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var t = serializer.Deserialize<string>(reader);
            if (t != null)
                return DateTime.ParseExact(t, "yyyyMMdd", CultureInfo.InvariantCulture);
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null && value is DateTime)
            {
                var p = ((DateTime)value).ToString("yyyyMMdd");
                serializer.Serialize(writer, p);
            }
        }
    }
}