using System.Linq;
using System.Threading.Tasks;

namespace Baidu.Yingyan.Track
{
    /// <summary>
    /// 轨迹上传
    /// <a href="http://lbsyun.baidu.com/index.php?title=yingyan/api/v3/trackupload">轨迹上传</a>
    /// </summary>
    public partial class TrackApi
    {
        private YingyanApi framework;
        private const string url = "track/";

        public TrackApi(YingyanApi framework)
        {
            this.framework = framework;
        }

        /// <summary>
        /// 为一个track添加最新轨迹点。
        /// </summary>
        /// <param name="entity_name">entity唯一标识</param>
        /// <param name="columns">开发者自定义字段(可选)</param>
        /// <param name="point">坐标</param>
        /// <returns></returns>
        public async Task<CommonResult> addpoint(TrackPoint point)
        {
            var args = framework.getNameValueCollection(point?.columns.ToDictionary(o => o.Key, o => o.Value?.ToString()));
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
    }
}