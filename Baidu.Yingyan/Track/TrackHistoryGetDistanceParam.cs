using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Baidu.Yingyan.Track
{
    /// <summary>
    /// 查询轨迹里程参数
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.Track.TrackHistoryGetLatestPointParam" />
    public class TrackHistoryGetDistanceParam : TrackHistoryGetLatestPointParam
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        [Required]
        public DateTime start_time { get; set; }

        /// <summary>
        /// 结束时间
        /// 结束时间不能大于当前时间，且起止时间区间不超过24小时。为提升响应速度，同时避免轨迹点过多造成请求超时（3s）失败，建议缩短每次请求的时间区间，将一天轨迹拆分成多段进行拼接
        /// </summary>
        [Required]
        public DateTime end_time { get; set; }

        /// <summary>
        /// 是否返回纠偏后里程
        /// </summary>
        public bool is_processed { get; set; }

        /// <summary>
        /// 里程补偿方式
        /// </summary>
        public TrackHistorySupplementModeEnums supplement_mode { get; set; }

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public override Dictionary<string, string> FillArgs(Dictionary<string, string> args)
        {
            args = base.FillArgs(args);
            args["start_time"] = start_time.ToUtcTicks().ToString();
            args["end_time"] = end_time.ToUtcTicks().ToString();
            args["is_processed"] = is_processed ? "1" : "0";
            args["supplement_mode"] = supplement_mode.ToString();
            return args;
        }
    }

    /// <summary>
    /// 里程补偿方式
    /// </summary>
    public enum TrackHistorySupplementModeEnums
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
}