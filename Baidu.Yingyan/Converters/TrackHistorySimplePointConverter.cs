using io.nulldata.Baidu.Yingyan.Track;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io.nulldata.Baidu.Yingyan.Converters
{
    /// <summary>
    /// 数组存储的经纬度和 LocationPoint 相互转换
    /// </summary>
    public class TrackHistorySimplePointConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var t = serializer.Deserialize<double[]>(reader);
            if (t != null && t.Length >= 4)
                return new TrackHistorySimplePoint() { longitude = t[0], latitude = t[1], loc_time = ((long)t[2]).FromUtcTicks(), speed = t[3] };
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null && value is TrackHistorySimplePoint)
            {
                var v = value as TrackHistorySimplePoint;
                double[] p = new double[] { v.longitude, v.latitude, v.loc_time.ToUtcTicks(), v.speed ?? 0 };
                serializer.Serialize(writer, p);
            }
        }
    }
}
