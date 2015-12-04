﻿using Newtonsoft.Json;
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
    public class LocationPointToStringConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var t = serializer.Deserialize<string>(reader).Split(',');
            if (t != null && t.Length >= 2)
                return new LocationPoint() { longitude = double.Parse(t[0]), latitude = double.Parse(t[1]), };
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null && value is LocationPoint)
            {
                var v = value as LocationPoint;
                double[] p = new double[] { v.longitude, v.latitude };
                serializer.Serialize(writer, string.Join(",", p));
            }
        }
    }
}
