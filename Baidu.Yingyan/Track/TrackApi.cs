using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Baidu.Yingyan.Track
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
        /// <param name="entity_name">entity唯一标识</param>
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
        /// 对于一个track批量上传轨迹点。按照时间顺序保留最后一个点作为实时点，过程耗时等信息。
        /// </summary>
        /// <param name="entity_name">entity唯一标识</param>
        /// <param name="points">坐标，官方文档说支持200个，实际测试大于100个就很难提交成功，建议一次50个点以下。</param>
        /// <returns></returns>
        public async Task<BatchAddPointResult> addpoints(string entity_name, params TrackPoint[] points)
        {
            //PS1：百度对 multipart/form-data 的支持并不太好，需要有固定的格式才能解析。
            //PS2：官方说支持200个点，但其实提交超过100个甚至更少都很容易出错，建议50个以下。
            string bodyFormat = @"------WebKitFormBoundary7MA4YWxkTrZu0gW
Content-Disposition:form-data;name=""ak""

{0}
------WebKitFormBoundary7MA4YWxkTrZu0gW
Content-Disposition:form-data;name=""service_id""

{1}
------WebKitFormBoundary7MA4YWxkTrZu0gW
Content-Disposition:form-data;name=""entity_name""

{2}
------WebKitFormBoundary7MA4YWxkTrZu0gW
Content-Disposition:form-data;name=""point_list"";filename=""point_list.csv""
Content-Type:application/vnd.ms-excel

{3}
------WebKitFormBoundary7MA4YWxkTrZu0gW";


            var data = string.Join(Environment.NewLine,
                new string[] { "longitude,latitude,loc_time,coord_type" }
                .Union(points.Select(o => o.ToString())));



            string body = string.Format(bodyFormat, framework.ak, framework.service_id, entity_name, data);
            var content = new StringContent(body);

            var header = new MediaTypeHeaderValue("multipart/form-data");
            header.Parameters.Add(new NameValueHeaderValue("boundary", "----WebKitFormBoundary7MA4YWxkTrZu0gW"));
            content.Headers.ContentType = header;

            return await YingyanApi.post<BatchAddPointResult>(url, "addpoints", content);

        }


        /// <summary>
        /// 通过service _id和entity_name查找本entity历史轨迹点的具体信息，包括经纬度，时间，其他用户自定义信息等。
        /// </summary>
        /// <param name="entity_name">必选</param>
        /// <param name="page_index">可选，默认值为1。page_index与page_size一起计算从第几条结果返回，代表返回第几页。</param>
        /// <param name="page_size">可选，默认值为100。page_size与page_index一起计算从第几条结果返回，代表返回结果中每页有几条记录</param>
        /// <returns></returns>
        public async Task<TrackHistoryResult> gethistory(string entity_name, DateTime start_time, DateTime end_time, bool sort_desc = true, bool is_processed = false, TrackHistoryProcessOption process_option = null, TrackHistorySupplementMode? supplement_mode = null, int page_index = 1, int page_size = 100)
        {
            return await gethistory_base(entity_name, start_time, end_time, false, sort_desc, is_processed, process_option, supplement_mode, page_index, page_size) as TrackHistoryResult;
        }

        /// <summary>
        /// 通过service _id和entity_name查找本entity历史轨迹点的具体信息，包括经纬度，时间，其他用户自定义信息等。
        /// </summary>
        /// <param name="entity_name">必选</param>
        /// <param name="page_index">可选，默认值为1。page_index与page_size一起计算从第几条结果返回，代表返回第几页。</param>
        /// <param name="page_size">可选，默认值为100。page_size与page_index一起计算从第几条结果返回，代表返回结果中每页有几条记录</param>
        /// <returns></returns>
        public async Task<TrackHistorySimpleResult> gethistory_simple(string entity_name, DateTime start_time, DateTime end_time, bool sort_desc = true, bool is_processed = false, TrackHistoryProcessOption process_option = null, TrackHistorySupplementMode? supplement_mode = null, int page_index = 1, int page_size = 100)
        {
            return await gethistory_base(entity_name, start_time, end_time, true, sort_desc, is_processed, process_option, supplement_mode, page_index, page_size) as TrackHistorySimpleResult;
        }
        /// <summary>
        /// 通过service _id和entity_name查找本entity历史轨迹点的具体信息，包括经纬度，时间，其他用户自定义信息等。
        /// </summary>
        /// <param name="entity_name">必选</param>
        /// <param name="page_index">可选，默认值为1。page_index与page_size一起计算从第几条结果返回，代表返回第几页。</param>
        /// <param name="page_size">可选，默认值为100。page_size与page_index一起计算从第几条结果返回，代表返回结果中每页有几条记录</param>
        /// <returns></returns>
        internal async Task<CommonResult> gethistory_base(string entity_name, DateTime start_time, DateTime end_time, bool simple_return = false, bool sort_desc = true, bool is_processed = false, TrackHistoryProcessOption process_option = null, TrackHistorySupplementMode? supplement_mode = null, int page_index = 1, int page_size = 100)
        {
            if (page_size > 5000)
                page_size = 5000;
            var nv = framework.getNameValueCollection();
            nv.Add("entity_name", entity_name);

            nv.Add("start_time", start_time.ToUtcTicks().ToString());
            nv.Add("end_time", end_time.ToUtcTicks().ToString());

            nv.Add("simple_return", simple_return ? "1" : "0");
            nv.Add("sort_type", sort_desc ? "0" : "1");
            nv.Add("is_processed", is_processed ? "1" : "0");
            if (process_option != null)
                nv.Add("process_option", process_option.ToString());
            if (supplement_mode != null)
                nv.Add("supplement_mode", supplement_mode.ToString());

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
