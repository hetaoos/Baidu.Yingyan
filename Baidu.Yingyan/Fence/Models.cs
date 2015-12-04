using io.nulldata.Baidu.Yingyan.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io.nulldata.Baidu.Yingyan.Fence
{
    public class TrackPoint
    {
        /// <summary>
        /// 纬度
        /// </summary>
        public double latitude { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double longitude { get; set; }
        /// <summary>
        /// 坐标类型
        /// </summary>
        public CoordType coord_type { get; set; }
        /// <summary>
        /// 轨迹点采集的GPS时间
        /// </summary>
        public DateTime loc_time { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", longitude, latitude, loc_time.ToUtcTicks(), (int)coord_type);
        }
    }


    public class TrackHistoryResult : CommonResult
    {
        public int size { get; set; }
        public int total { get; set; }
        public string entity_name { get; set; }
        public int distance { get; set; }
        public TrackHistoryPoint[] points { get; set; }
    }

    public class TrackHistoryPoint
    {
        [JsonConverter(typeof(UnixTicksConverter))]
        public DateTime loc_time { get; set; }
        public float[] location { get; set; }
        public DateTime create_time { get; set; }
        public double? speed { get; set; }
        public double? radius { get; set; }
        public int? direction { get; set; }

        [JsonExtensionData]
        public IDictionary<string, object> columns { get; set; }
    }

    public class TrackHistorySimpleResult : CommonResult
    {
        public int size { get; set; }
        public int total { get; set; }
        public string entity_name { get; set; }
        public int distance { get; set; }
        public double[][] points { get; set; }
    }


    public class TrackColumnListResult : CommonResult
    {
        public TrackColumn[] columns { get; set; }
    }

    public class TrackColumn
    {
        public TrackColumnType column_type  { get; set; }
        public DateTime create_time { get; set; }
        public string column_key { get; set; }
        public string column_desc { get; set; }
    }


    public enum TrackColumnType
    {
        Int64 = 1,
        Double = 2,
        String = 3,
    }
}
