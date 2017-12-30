using System.Collections.Generic;

namespace Baidu.Yingyan.Track
{
    /// <summary>
    /// 轨迹查询与纠偏参数
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.Track.TrackHistoryGetDistanceParam" />
    public class TrackHistoryGetTrackParam : TrackHistoryGetDistanceParam
    {
        /// <summary>
        /// 返回轨迹点的排序规则
        /// </summary>
        public bool asc { get; set; } = true;

        /// <summary>
        /// 可选，默认值为1。page_index与page_size一起计算从第几条结果返回，代表返回第几页。
        /// </summary>
        public int page_index { get; set; } = 1;

        /// <summary>
        /// 可选，默认值为100。最大值5000。page_size与page_index一起计算从第几条结果返回，代表返回结果中每页有几条记录。
        /// </summary>
        public int page_size { get; set; } = 100;

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public override Dictionary<string, string> FillArgs(Dictionary<string, string> args)
        {
            args = base.FillArgs(args);
            args["sort_type"] = asc ? "asc" : "desc";
            args["page_index"] = page_index.ToString();
            args["page_size"] = page_size.ToString();
            return args;
        }
    }
}