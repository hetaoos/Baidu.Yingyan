using Baidu.Yingyan.Entity;

namespace Baidu.Yingyan.Track
{
    /// <summary>
    /// 实时纠偏结果
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.CommonResult" />
    public class TrackHistoryGetLatestPointResult : CommonResult
    {
        /// <summary>
        ///实时位置信息
        /// </summary>
        public EntityLocationPoint latest_point { get; set; }

        /// <summary>
        /// 道路限速,单位：km/h
        /// </summary>
        public double limit_speed { get; set; }
    }
}