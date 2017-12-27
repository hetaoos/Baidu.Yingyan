using System.Linq;
using System.Threading.Tasks;

namespace Baidu.Yingyan.Track
{
    /// <summary>
    /// 为 entity 上传轨迹点，支持为一个 entity上传一个或多个轨迹点，也支持为多个 entity 上传多个轨迹点。
    /// 轨迹纠偏类接口为开发者提供轨迹去噪、抽稀、绑路功能，包括实时位置纠偏、轨迹纠偏、里程计算功能。
    /// <a href="http://lbsyun.baidu.com/index.php?title=yingyan/api/v3/trackupload">轨迹上传</a>
    /// <a href="http://lbsyun.baidu.com/index.php?title=yingyan/api/v3/trackprocess">轨迹查询和纠偏</a>
    /// </summary>
    public partial class TrackApi
    {
        private YingyanApi framework;
        private const string url = "track/";

        public TrackApi(YingyanApi framework)
        {
            this.framework = framework;
        }

        #region 上传轨迹点

        /// <summary>
        /// 为一个track添加最新轨迹点。
        /// </summary>
        /// <param name="entity_name">entity唯一标识</param>
        /// <param name="columns">开发者自定义字段(可选)</param>
        /// <param name="point">坐标</param>
        /// <returns></returns>
        public async Task<CommonResult> addpoint(TrackPoint point)
        {
            var args = framework.getNameValueCollection(point?.columns?.ToDictionary(o => o.Key, o => o.Value?.ToString()));
            args["entity_name"] = point.entity_name;
            args["latitude"] = point.latitude.ToString();
            args["longitude"] = point.longitude.ToString();
            args["loc_time"] = point.loc_time.ToUtcTicks().ToString();
            args["coord_type_input"] = point.coord_type_input.ToString();
            if (point.speed > 0)
                args["speed"] = point.speed.ToString();
            if (point.direction > 0)
                args["direction"] = point.direction.ToString();
            if (point.height > 0)
                args["height"] = point.height.ToString();
            if (point.radius > 0)
                args["radius"] = point.radius.ToString();
            if (string.IsNullOrWhiteSpace(point.object_name) == false)
                args["object_name"] = point.object_name;

            return await framework.post<CommonResult>(url + "addpoint", args);
        }

        /// <summary>
        /// 对于一个track批量上传轨迹点。按照时间顺序保留最后一个点作为实时点，过程耗时等信息。
        /// </summary>
        /// <param name="entity_name">entity唯一标识</param>
        /// <param name="points">坐标，轨迹点总数不超过100个，json 格式。轨迹点字段描述参见 </param>
        /// <returns></returns>
        public async Task<BatchAddPointResult> addpoints(TrackPoint[] points)
        {
            var args = framework.getNameValueCollection();
            if (points?.Any() == true)
                args["point_list"] = Newtonsoft.Json.JsonConvert.SerializeObject(points);

            return await framework.post<BatchAddPointResult>(url + "addpoints", args);
        }

        #endregion 上传轨迹点

        #region 轨迹纠偏

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

        #endregion 轨迹纠偏
    }
}