using Newtonsoft.Json;
using System;

namespace Baidu.Yingyan.Converters
{
    public class FloorConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override bool CanWrite => false;
        public override bool CanRead => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var t = serializer.Deserialize<string>(reader);
            if (string.IsNullOrWhiteSpace(t))
                return null;
            double d;
            if (double.TryParse(t, out d))
                return (int)d;
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}