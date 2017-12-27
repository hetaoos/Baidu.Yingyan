using System.Collections.Specialized;

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
        /// <param name="nv">The nv.</param>
        /// <returns></returns>
        public override NameValueCollection FillArgs(NameValueCollection nv)
        {
            nv = base.FillArgs(nv);
            nv.Add("sort_type", asc ? "asc" : "desc");
            nv.Add("page_index", page_index.ToString());
            nv.Add("page_size", page_size.ToString());
            return nv;
        }
    }
}