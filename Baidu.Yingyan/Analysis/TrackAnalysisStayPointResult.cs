using Baidu.Yingyan.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Baidu.Yingyan.Analysis
{
    /// <summary>
    /// 停留点分析结果
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.CommonResult" />
    public class TrackAnalysisStayPointResult : CommonResult
    {
        /// <summary>
        /// 停留次数
        /// </summary>
        public int staypoint_num { get; set; }

        /// <summary>
        /// 停留记录列表
        /// </summary>
        public List<TrackAnalysisStayPoint> stay_points { get; set; }
    }

    /// <summary>
    /// 停留点
    /// </summary>
    public class TrackAnalysisStayPoint
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        [JsonConverter(typeof(UnixTicksConverter))]
        public DateTime start_time { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [JsonConverter(typeof(UnixTicksConverter))]
        public DateTime end_time { get; set; }

        /// <summary>
        /// 停留时长，单位：秒
        /// </summary>
        public int duration { get; set; }

        /// <summary>
        /// 停留点
        /// </summary>
        public LocationPoint stay_point { get; set; }
    }
}