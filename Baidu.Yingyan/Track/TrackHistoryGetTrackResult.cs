using Baidu.Yingyan.Entity;
using System.Collections.Generic;

namespace Baidu.Yingyan.Track
{
    /// <summary>
    /// 轨迹查询与纠偏结果
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.CommonResult" />
    public class TrackHistoryGetTrackResult : CommonResult
    {
        /// <summary>
        /// 忽略掉page_index，page_size后的轨迹点数量
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 返回的结果条数
        /// </summary>
        public int size { get; set; }

        /// <summary>
        /// 此段轨迹的里程数，单位：米
        /// </summary>
        public double distance { get; set; }

        /// <summary>
        /// 此段轨迹的收费里程数，单位：米
        /// </summary>
        public double toll_distance { get; set; }

        /// <summary>
        /// 起点信息
        /// </summary>
        public LocationPointWithTime start_point { get; set; }

        /// <summary>
        /// 终点信息
        /// </summary>
        public LocationPointWithTime end_point { get; set; }

        /// <summary>
        /// 历史轨迹点列表
        /// </summary>
        public List<EntityLocationPoint> points { get; set; }
    }
}