using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace io.nulldata.Baidu.Yingyan.Track
{
    public class TrackApi
    {
        private YingyanApi framework;
        private Uri url = new Uri(YingyanApi.url + "track/");

        public TrackApi(YingyanApi framework)
        {
            this.framework = framework;
        }
        /// <summary>
        /// 为一个track添加最新轨迹点。
        /// </summary>
        /// <param name="entity_name"entity唯一标识</param>
        /// <param name="columns">开发者自定义字段(可选)</param>
        /// <param name="point">坐标</param>
        /// <returns></returns>
        public async Task<CommonResult> addpoint(string entity_name, TrackPoint point, Dictionary<string, string> columns = null)
        {
            var args = framework.getArgs(columns);
            args["entity_name"] = entity_name;
            args["latitude"] = point.latitude.ToString();
            args["longitude"] = point.longitude.ToString();
            args["coord_type"] = ((int)point.coord_type).ToString();
            args["loc_time"] = point.loc_time.ToUtcTicks().ToString();

            var content = new FormUrlEncodedContent(args);

            return await YingyanApi.post<CommonResult>(url, "addpoint", content, YingyanApi.getDefaultHttpError<CommonResult>());

        }

        /// <summary>
        /// 通过service _id和entity_name查找本entity历史轨迹点的具体信息，包括经纬度，时间，其他用户自定义信息等。
        /// </summary>
        /// <param name="entity_name">必选</param>
        /// <param name="page_index">可选，默认值为1。page_index与page_size一起计算从第几条结果返回，代表返回第几页。</param>
        /// <param name="page_size">可选，默认值为100。page_size与page_index一起计算从第几条结果返回，代表返回结果中每页有几条记录</param>
        /// <returns></returns>
        public async Task<TrackHistoryResult> gethistory(string entity_name, DateTime start_time, DateTime end_time, bool is_processed = false, int page_index = 1, int page_size = 100)
        {
            return await gethistory_base(entity_name, start_time, end_time, false, is_processed, page_index, page_size) as TrackHistoryResult;
        }

        /// <summary>
        /// 通过service _id和entity_name查找本entity历史轨迹点的具体信息，包括经纬度，时间，其他用户自定义信息等。
        /// </summary>
        /// <param name="entity_name">必选</param>
        /// <param name="page_index">可选，默认值为1。page_index与page_size一起计算从第几条结果返回，代表返回第几页。</param>
        /// <param name="page_size">可选，默认值为100。page_size与page_index一起计算从第几条结果返回，代表返回结果中每页有几条记录</param>
        /// <returns></returns>
        public async Task<TrackHistorySimpleResult> gethistory_simple(string entity_name, DateTime start_time, DateTime end_time, bool is_processed = false, int page_index = 1, int page_size = 100)
        {
            return await gethistory_base(entity_name, start_time, end_time, true, is_processed, page_index, page_size) as TrackHistorySimpleResult;
        }
        /// <summary>
        /// 通过service _id和entity_name查找本entity历史轨迹点的具体信息，包括经纬度，时间，其他用户自定义信息等。
        /// </summary>
        /// <param name="entity_name">必选</param>
        /// <param name="page_index">可选，默认值为1。page_index与page_size一起计算从第几条结果返回，代表返回第几页。</param>
        /// <param name="page_size">可选，默认值为100。page_size与page_index一起计算从第几条结果返回，代表返回结果中每页有几条记录</param>
        /// <returns></returns>
        internal async Task<CommonResult> gethistory_base(string entity_name, DateTime start_time, DateTime end_time, bool simple_return = false, bool is_processed = false, int page_index = 1, int page_size = 100)
        {
            var nv = framework.getNameValueCollection();
            nv.Add("entity_name", entity_name);

            nv.Add("start_time", start_time.ToUtcTicks().ToString());
            nv.Add("end_time", end_time.ToUtcTicks().ToString());

            nv.Add("simple_return", simple_return ? "1" : "0");
            nv.Add("is_processed", is_processed ? "1" : "0");

            nv.Add("page_index", page_index.ToString());
            nv.Add("page_size", page_size.ToString());
            if (simple_return)
                return await YingyanApi.get<TrackHistorySimpleResult>(url, "gethistory", nv, YingyanApi.getDefaultHttpError<TrackHistorySimpleResult>());
            else
                return await YingyanApi.get<TrackHistoryResult>(url, "gethistory", nv, YingyanApi.getDefaultHttpError<TrackHistoryResult>());

        }

        /// <summary>
        /// 为entity添加一个属性字段，字段只能为字符类型，支持最大长度为128。
        /// </summary>
        /// <param name="column_key">必选，最多创建5个属性字段，同一个service下entity的column_key不能重复。</param>
        /// <param name="column_desc">字段描述</param>
        /// <param name="column_type">可选。必选，枚举值1:Int64, 2:double, 3:string（字符串最大支持2048字符）</param>
        /// <returns></returns>
        public async Task<CommonResult> add_column(string column_key, string column_desc = null, TrackColumnType column_type = TrackColumnType.String)
        {
            var args = framework.getArgs();
            args["column_key"] = column_key;
            if (string.IsNullOrEmpty(column_desc) == false)
                args["column_desc"] = column_desc;
            args["column_type"] = ((int)column_type).ToString();
            var content = new FormUrlEncodedContent(args);

            return await YingyanApi.post<CommonResult>(url, "addcolumn", content, YingyanApi.getDefaultHttpError<CommonResult>());

        }

        /// <summary>
        /// 为entity添加一个属性字段，字段只能为字符类型，支持最大长度为128。
        /// </summary>
        /// <param name="entity_name">entity名称，作为其唯一标识。</param>
        /// <param name="column_key">必选，最多创建5个属性字段，同一个service下entity的column_key不能重复。</param>
        /// <returns></returns>
        public async Task<CommonResult> delete_column(string entity_name, string column_key)
        {
            var args = framework.getArgs();
            args["entity_name"] = entity_name;
            args["column_key"] = column_key;
            var content = new FormUrlEncodedContent(args);

            return await YingyanApi.post<CommonResult>(url, "deletecolumn", content, YingyanApi.getDefaultHttpError<CommonResult>());

        }

        /// <summary>
        /// 列出entity所有自定义属性字段。
        /// </summary>
        /// <returns></returns>
        public async Task<TrackColumnListResult> list_column()
        {
            var nv = framework.getNameValueCollection();
            return await YingyanApi.get<TrackColumnListResult>(url, "listcolumn", nv, YingyanApi.getDefaultHttpError<TrackColumnListResult>());


        }

    }
}
