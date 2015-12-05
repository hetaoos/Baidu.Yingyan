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

namespace io.nulldata.Baidu.Yingyan.Entity
{
    public class EntityApi
    {
        private YingyanApi framework;
        private Uri url = new Uri(YingyanApi.url + "entity/");

        public EntityApi(YingyanApi framework)
        {
            this.framework = framework;
        }
        /// <summary>
        /// 添加一个新的entity，一个entity可以是一个人、一辆车、或者任何运动的物体。
        /// </summary>
        /// <param name="entity_name">添加一个新的entity，一个entity可以是一个人、一辆车、或者任何运动的物体。</param>
        /// <param name="columns">开发者自定义字段(可选)</param>
        /// <returns></returns>
        public async Task<CommonResult> add(string entity_name, Dictionary<string, string> columns = null)
        {
            var args = framework.getArgs(columns);
            args["entity_name"] = entity_name;
            var content = new FormUrlEncodedContent(args);

            return await YingyanApi.post<CommonResult>(url, "add", content, YingyanApi.getDefaultHttpError<CommonResult>());

        }

        /// <summary>
        /// 根据entity_name删除一个entity。
        /// </summary>
        /// <param name="entity_name">entity名称，作为其唯一标识。</param>
        /// <returns></returns>
        public async Task<CommonResult> delete(string entity_name)
        {
            var args = framework.getArgs();
            args["entity_name"] = entity_name;
            var content = new FormUrlEncodedContent(args);
            return await YingyanApi.post<CommonResult>(url, "delete", content, YingyanApi.getDefaultHttpError<CommonResult>());

        }

        /// <summary>
        /// 更新entity信息。
        /// </summary>
        /// <param name="entity_name">entity名称，作为其唯一标识。</param>
        /// <param name="columns">开发者自定义字段(可选)</param>
        /// <returns></returns>
        public async Task<CommonResult> update(string entity_name, Dictionary<string, string> columns = null)
        {
            var args = framework.getArgs(columns);
            args["entity_name"] = entity_name;
            var content = new FormUrlEncodedContent(args);

            return await YingyanApi.post<CommonResult>(url, "update", content, YingyanApi.getDefaultHttpError<CommonResult>());

        }

        /// <summary>
        /// 根据service_id、entity_name和自定义检索字段，查询本service中所有符合条件的entity信息及其实时位置。
        /// </summary>
        /// <param name="entity_names">可选，支持输入多个entity_name，以英文 ’,’ 逗号分开，如： car01,car02,car03</param>
        /// <param name="columns">开发者自定义的entity属性字段</param>
        /// <param name="active_time">活跃时间,可选，指定该字段时,返回从该时间点之后仍有位置变动的entity。</param>
        /// <param name="page_index">可选，默认值为1。page_index与page_size一起计算从第几条结果返回，代表返回第几页。</param>
        /// <param name="page_size">可选，默认值为100。page_size与page_index一起计算从第几条结果返回，代表返回结果中每页有几条记录</param>
        /// <returns></returns>
        public async Task<EntityListReault> list(string[] entity_names = null, Dictionary<string, string> columns = null, DateTime? active_time = null, int page_index = 1, int page_size = 100)
        {
            return await list_base(entity_names, columns, active_time, false, page_index, page_size) as EntityListReault;
        }

        /// <summary>
        /// 根据service_id、entity_name和自定义检索字段，查询本service中所有符合条件的entity信息及其实时位置。
        /// </summary>
        /// <param name="entity_names">可选，支持输入多个entity_name，以英文 ’,’ 逗号分开，如： car01,car02,car03</param>
        /// <param name="columns">开发者自定义的entity属性字段</param>
        /// <param name="active_time">活跃时间,可选，指定该字段时,返回从该时间点之后仍有位置变动的entity。</param>
        /// <param name="page_index">可选，默认值为1。page_index与page_size一起计算从第几条结果返回，代表返回第几页。</param>
        /// <param name="page_size">可选，默认值为100。page_size与page_index一起计算从第几条结果返回，代表返回结果中每页有几条记录</param>
        /// <returns></returns>
        public async Task<EntityListSimpleReault> list_simple(string[] entity_names = null, Dictionary<string, string> columns = null, DateTime? active_time = null, int page_index = 1, int page_size = 100)
        {
            return await list_base(entity_names, columns, active_time, true, page_index, page_size) as EntityListSimpleReault;
        }

        /// <summary>
        /// 根据service_id、entity_name和自定义检索字段，查询本service中所有符合条件的entity信息及其实时位置。
        /// </summary>
        /// <param name="entity_names">可选，支持输入多个entity_name，以英文 ’,’ 逗号分开，如： car01,car02,car03</param>
        /// <param name="columns">开发者自定义的entity属性字段</param>
        /// <param name="active_time">活跃时间,可选，指定该字段时,返回从该时间点之后仍有位置变动的entity。</param>
        /// <param name="only_return_entity_name">可选。0代表返回全部结果，1代表只返回entity_name字段。默认值为0。</param>
        /// <param name="page_index">可选，默认值为1。page_index与page_size一起计算从第几条结果返回，代表返回第几页。</param>
        /// <param name="page_size">可选，默认值为100。page_size与page_index一起计算从第几条结果返回，代表返回结果中每页有几条记录</param>
        /// <returns></returns>
        internal async Task<CommonResult> list_base(string[] entity_names = null, Dictionary<string, string> columns = null, DateTime? active_time = null, bool only_return_entity_name = false, int page_index = 1, int page_size = 100)
        {
            var nv = framework.getNameValueCollection(columns);
            if (entity_names != null && entity_names.Length > 0)
                nv.Add("entity_names", string.Join(",", entity_names));
            if (active_time != null)
                nv.Add("active_time", active_time.Value.ToUtcTicks().ToString());
            nv.Add("return_type", only_return_entity_name ? "1" : "0");

            nv.Add("page_index", page_index.ToString());
            nv.Add("page_size", page_size.ToString());
            if (only_return_entity_name)
                return await YingyanApi.get<EntityListSimpleReault>(url, "list", nv, YingyanApi.getDefaultHttpError<EntityListSimpleReault>());
            else
                return await YingyanApi.get<EntityListReault>(url, "list", nv, YingyanApi.getDefaultHttpError<EntityListReault>());

        }

        /// <summary>
        /// 为entity添加一个属性字段，字段只能为字符类型，支持最大长度为128。
        /// </summary>
        /// <param name="column_key">必选，最多创建5个属性字段，同一个service下entity的column_key不能重复。</param>
        /// <param name="column_desc">字段描述</param>
        /// <param name="is_search">可选。1代表检索字段，0代表非检索字段。最多设置2个检索字段，且字段一经创建，此属性将不可更改。</param>
        /// <returns></returns>
        public async Task<CommonResult> add_column(string column_key, string column_desc = null, bool? is_search = null)
        {
            var args = framework.getArgs();
            args["column_key"] = column_key;
            if (string.IsNullOrEmpty(column_desc) == false)
                args["column_desc"] = column_desc;
            if (is_search.HasValue)
                args["is_search"] = is_search.HasValue ? "1" : "0";
            var content = new FormUrlEncodedContent(args);

            return await YingyanApi.post<CommonResult>(url, "addcolumn", content, YingyanApi.getDefaultHttpError<CommonResult>());

        }

        /// <summary>
        /// 为entity添加一个属性字段，字段只能为字符类型，支持最大长度为128。
        /// </summary>
        /// <param name="column_key">必选，最多创建5个属性字段，同一个service下entity的column_key不能重复。</param>
        /// <returns></returns>
        public async Task<CommonResult> delete_column(string column_key)
        {
            var args = framework.getArgs();
            args["column_key"] = column_key;
            var content = new FormUrlEncodedContent(args);

            return await YingyanApi.post<CommonResult>(url, "deletecolumn", content, YingyanApi.getDefaultHttpError<CommonResult>());

        }

        /// <summary>
        /// 列出entity所有自定义属性字段。
        /// </summary>
        /// <returns></returns>
        public async Task<EntityColumnListResult> list_column()
        {
            var nv = framework.getNameValueCollection();
            return await YingyanApi.get<EntityColumnListResult>(url, "listcolumn", nv, YingyanApi.getDefaultHttpError<EntityColumnListResult>());


        }

    }
}
