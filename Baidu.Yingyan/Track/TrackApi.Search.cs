using System.Threading.Tasks;

namespace Baidu.Yingyan.Track
{
    /// <summary>
    /// 轨迹上传
    /// <a href="http://lbsyun.baidu.com/index.php?title=yingyan/api/v3/trackprocess">轨迹纠偏</a>
    /// </summary>
    public partial class TrackApi
    {
        /// <summary>
        /// 查询某 entity 的实时位置，支持纠偏
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<TrackHistoryGetLatestPointResult> getlatestpoint(TrackHistoryGetLatestPointParam param)
        {
            return framework.get<TrackHistoryGetLatestPointResult>(url + "getlatestpoint", param);
        }

        /// <summary>
        /// 查询某 entity 一段时间内的轨迹里程，支持纠偏
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<TrackHistoryGetDistanceResult> getdistance(TrackHistoryGetDistanceParam param)
        {
            return framework.get<TrackHistoryGetDistanceResult>(url + "getdistance", param);
        }

        /// <summary>
        /// 查询某 entity 一段时间内的轨迹点，支持纠偏
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<TrackHistoryGetTrackResult> gettrack(TrackHistoryGetTrackParam param)
        {
            return framework.get<TrackHistoryGetTrackResult>(url + "gettrack", param);
        }
    }
}