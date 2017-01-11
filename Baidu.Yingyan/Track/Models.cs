using Baidu.Yingyan.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baidu.Yingyan.Track
{
    public class TrackPoint : LocationPoint
    {
        /// <summary>
        /// 坐标类型
        /// </summary>
        public CoordType coord_type { get; set; }
        /// <summary>
        /// 轨迹点采集的GPS时间
        /// </summary>
        public DateTime loc_time { get; set; }

        [JsonExtensionData]
        public IDictionary<string, object> columns { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", longitude, latitude, loc_time.ToUtcTicks(), (int)coord_type);
        }
    }

    public class BatchAddPointResult : CommonResult
    {
        public int time { get; set; }

        public List<TrackPoint> error_points { get; set; }
    }

    /// <summary>
    /// 纠偏选项
    /// </summary>
    public class TrackHistoryProcessOption
    {
        /// <summary>
        /// 去噪，默认为1
        /// </summary>
        public bool? need_denoise { get; set; }
        /// <summary>
        ///   抽稀，默认为1
        /// </summary>
        public bool? need_vacuate { get; set; }

        /// <summary>
        /// 绑路，之前未开通绑路的service，默认值为0；之前已开通绑路的service，默认值为1
        /// </summary>
        public bool? need_mapmatch { get; set; }
        /// <summary>
        /// 交通方式
        /// </summary>
        public TrackHistoryTransportMode? transport_mode { get; set; }
        /// <summary>
        /// 获取选项值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetOption(string name, bool value)
        {
            var t = value == true ? 1 : 0;
            return $"{name}={t}";
        }
        public override string ToString()
        {
            var options = new List<string>();
            if (need_denoise != null)
                options.Add(GetOption(nameof(need_denoise), need_denoise.Value));
            if (need_vacuate != null)
                options.Add(GetOption(nameof(need_vacuate), need_vacuate.Value));
            if (need_mapmatch != null)
                options.Add(GetOption(nameof(need_mapmatch), need_mapmatch.Value));
            if (transport_mode != null)
                options.Add($"{nameof(transport_mode)}={(int)transport_mode}");
            return string.Join(",", options);
        }
    }

    /// <summary>
    /// 交通方式
    /// </summary>
    public enum TrackHistoryTransportMode
    {
        /// <summary>
        /// 驾车(默认)
        /// </summary>
        driving = 1,
        /// <summary>
        ///  骑行
        /// </summary>
        riding = 2,
        /// <summary>
        /// 步行
        /// </summary>
        walking = 3,
    }

    /// <summary>
    /// 里程补偿方式
    /// </summary>
    public enum TrackHistorySupplementMode
    {
        /// <summary>
        /// 不补充，中断两点间距离不记入里程。
        /// </summary>
        no_supplement,
        /// <summary>
        ///  使用直线距离补充
        /// </summary>
        straight,
        /// <summary>
        /// 使用最短驾车路线距离补充
        /// </summary>
        driving,
        /// <summary>
        ///  使用最短骑行路线距离补充
        /// </summary>
        riding,
        /// <summary>
        /// 使用最短步行路线距离补充
        /// </summary>
        walking,
    }


    public class TrackHistoryResult : CommonResult
    {
        public int size { get; set; }
        public int total { get; set; }
        public string entity_name { get; set; }
        public double distance { get; set; }
        public TrackHistoryPoint[] points { get; set; }
    }

    /// <summary>
    /// 历史记录点
    /// </summary>
    public class TrackHistoryPoint
    {
        /// <summary>
        /// 定位时的设备时间
        /// </summary>
        [JsonConverter(typeof(UnixTicksConverter))]
        public DateTime loc_time { get; set; }
        [JsonConverter(typeof(LocationPointToArrayConverter))]
        public LocationPoint location { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime create_time { get; set; }
        /// <summary>
        /// 速度
        /// </summary>
        public double? speed { get; set; }
        /// <summary>
        /// 定位精度
        /// </summary>
        public double? radius { get; set; }
        /// <summary>
        /// 方向
        /// </summary>
        public int? direction { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? height { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(FloorConverter))]
        public int? floor { get; set; }

        /// <summary>
        /// 填充点
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? _supplement { get; set; }
        /// <summary>
        /// 绑路失败
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? _mapmatch_failed { get; set; }

        [JsonExtensionData]
        public IDictionary<string, object> columns { get; set; }
    }

    public class TrackHistorySimpleResult : CommonResult
    {
        public int size { get; set; }
        public int total { get; set; }
        public string entity_name { get; set; }
        public double distance { get; set; }
        public List<TrackHistorySimplePoint> points { get; set; }
    }
    [JsonConverter(typeof(TrackHistorySimplePointConverter))]
    public class TrackHistorySimplePoint : LocationPoint
    {
        public DateTime loc_time { get; set; }
        public double? speed { get; set; }
    }


    public class TrackColumnListResult : CommonResult
    {
        public TrackColumn[] columns { get; set; }
    }

    public class TrackColumn
    {
        public TrackColumnType column_type { get; set; }
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
