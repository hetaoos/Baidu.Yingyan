using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baidu.Yingyan.Converters
{
    /// <summary>
    /// 数组存储的经纬度和 LocationPoint 相互转换
    /// </summary>
    public class LocationPointToStringConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType == typeof(LocationPoint) || objectType.IsSubclassOf(typeof(LocationPoint)))
            {
                var t = serializer.Deserialize<string>(reader).Split(',');
                if (t != null && t.Length >= 2)
                    return new LocationPoint() { longitude = double.Parse(t[0]), latitude = double.Parse(t[1]), };
                return null;
            }
            else
            {
                var arr = serializer.Deserialize<string>(reader).Split(';')
                    .Select(o => o.Split(','))
                    .Select(o => new LocationPoint() { longitude = double.Parse(o[0]), latitude = double.Parse(o[1]) });

                if (objectType.GetInterfaces().Contains(typeof(IList)))
                    return arr.ToList();
                else
                    return arr;
            }
         
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null )
            {
                if (value is LocationPoint)
                {
                    var v = value as LocationPoint;
                    serializer.Serialize(writer, LocationPointToString(v));
                }else
                {
                    string s = null;
                    if (value is IEnumerable<LocationPoint>)
                        s = string.Join(";", (value as IEnumerable<LocationPoint>).Select(o => LocationPointToString(o)).ToArray());
                    else if (value is string[])
                    {
                        s = string.Join(";", (value as LocationPoint[]).Select(o => LocationPointToString(o)).ToArray());
                    }
                    serializer.Serialize(writer, s);
                }
            }
        }
        private string LocationPointToString(LocationPoint p)
        {
            return string.Format("{0},{1}", p.longitude, p.latitude);
        }
    }
}
