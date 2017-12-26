using Baidu.Yingyan.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Baidu.Yingyan.Entity
{
    /// <summary>
    /// entity 搜索类接口通用返回结果
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.CommonResult" />
    public class EntityListReault : CommonResult
    {
        /// <summary>
        /// 本页返回的结果条数
        /// </summary>
        public int size { get; set; }

        /// <summary>
        /// 本次检索总结果条数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// entity详细信息列表
        /// </summary>
        public EntityItem[] entities { get; set; }
    }

    /// <summary>
    /// entity 对象
    /// </summary>
    public class EntityItem
    {
        /// <summary>
        /// entity名称，其唯一标识
        /// </summary>
        public string entity_name { get; set; }

        /// <summary>
        /// entity 可读性描述
        /// </summary>
        public string entity_desc { get; set; }

        /// <summary>
        /// entity属性修改时间，该时间为服务端时间
        /// </summary>
        public DateTime modify_time { get; set; }

        /// <summary>
        /// entity创建时间，该时间为服务端时间
        /// </summary>
        public DateTime create_time { get; set; }

        /// <summary>
        /// 最新的轨迹点信息
        /// </summary>
        public EntityLocationPoint latest_location { get; set; }

        /// <summary>
        /// 开发者自定义的entity属性信息
        /// </summary>
        [JsonExtensionData]
        public IDictionary<string, object> columns { get; set; }
    }

    /// <summary>
    /// 最新的轨迹点信息
    /// </summary>
    public class EntityLocationPoint : LocationPoint
    {
        /// <summary>
        /// 定位精度(m)
        /// </summary>
        public double? radius { get; set; }

        /// <summary>
        /// 该entity最新定位时间
        /// </summary>
        [JsonConverter(typeof(UnixTicksConverter))]
        public DateTime loc_time { get; set; }

        /// <summary>
        /// 方向,范围为[0,359]，0度为正北方向，顺时针
        /// </summary>
        public int? direction { get; set; }

        /// <summary>
        /// 速度,(km/h)
        /// </summary>
        public double? speed { get; set; }

        /// <summary>
        /// 高度,(m)
        /// </summary>
        public double? height { get; set; }

        /// <summary>
        /// 楼层,若处于百度支持室内定位的区域，则将返回楼层信息，默认 null
        /// </summary>
        public string floor { get; set; }

        /// <summary>
        /// 距中心点距离，单位：m，仅在周边搜索（entity/aroundsearch）时返回该字段
        /// </summary>
        public double? distance { get; set; }

        /// <summary>
        /// 对象数据名称
        /// </summary>
        public string object_name { get; set; }

        /// <summary>
        /// 开发者自定义track的属性，只有当开发者为track创建了自定义属性字段，且赋过值，才会返回
        /// </summary>
        [JsonExtensionData]
        public IDictionary<string, object> columns { get; set; }
    }

    /// <summary>
    /// 查询entity参数
    /// </summary>
    public class EntityListParam : IYingyanParam
    {
        /// <summary>
        /// entity_name列表，多个entity用逗号分隔，精确筛选。示例："entity_names:张三,李四"
        /// </summary>
        public string[] entity_names { get; set; }

        /// <summary>
        /// unix时间戳，查询在此时间之后有定位信息上传的entity（loc_time&gt;=active_time）。如查询2016-8-21 00:00:00之后仍活跃的entity，示例："active_time:1471708800"。active_time 和 inactive_time 不可同时输入
        /// </summary>
        public DateTime? active_time { get; set; }

        /// <summary>
        /// inactive_time：unix时间戳，查询在此时间之后无定位信息上传的entity（loc_time &lt; inactive_time）。如查询2016-8-21 00:00:00之后不活跃的entity示例："inactive_time:1471708800"。active_time 和 inactive_time 不可同时输入
        /// </summary>
        public DateTime? inactive_time { get; set; }

        /// <summary>
        /// 开发者自定义的可筛选的entity属性字段，示例："team:北京"
        /// </summary>
        public Dictionary<string, string> columns { get; set; }

        /// <summary>
        /// 返回结果的坐标类型，默认值：bd09
        /// </summary>
        public CoordTypeEnums coord_type_output { get; set; } = CoordTypeEnums.bd09ll;

        /// <summary>
        /// 可选，默认值为1。page_index与page_size一起计算从第几条结果返回，代表返回第几页。
        /// </summary>
        public int page_index { get; set; } = 1;

        /// <summary>
        /// 可选，默认值为100。最大值1000。page_size与page_index一起计算从第几条结果返回，代表返回结果中每页有几条记录。
        /// </summary>
        public int page_size { get; set; } = 100;

        /// <summary>
        /// 过滤条件(filter)
        /// </summary>
        /// <returns></returns>
        public virtual string GetFilter()
        {
            var filters = new Dictionary<string, string>();
            if (entity_names?.Any() == true)
                filters.Add("entity_names", string.Join(",", entity_names));

            if (active_time != null)
                filters.Add("active_time", active_time.Value.ToUtcTicks().ToString());
            else if (inactive_time != null)
                filters.Add("inactive_time", inactive_time.Value.ToUtcTicks().ToString());

            if (columns?.Any() == true)
            {
                foreach (var c in columns)
                    filters[c.Key] = c.Value;
            }
            if (filters.Any())
                return string.Join("|", filters.Select(o => $"{o.Key}:{o.Value}"));
            else
                return null;
        }

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">The nv.</param>
        /// <returns></returns>
        public virtual NameValueCollection FillArgs(NameValueCollection nv)
        {
            if (nv == null)
                nv = new NameValueCollection();
            var filter = GetFilter();
            if (string.IsNullOrWhiteSpace(filter) == false)
                nv.Add("filter", filter);

            nv.Add("coord_type_output", coord_type_output.ToString());
            nv.Add("page_index", page_index.ToString());
            nv.Add("page_size", page_size.ToString());

            return nv;
        }
    }

    /// <summary>
    /// 搜索参数
    /// </summary>
    public class EntityListWithOrderParam : EntityListParam
    {
        /// <summary>
        /// 默认值：entity_name:asc（按 entity_name 升序排序）
        /// 只支持按一个字段排序，支持的排序字段如下：
        /// loc_time：entity 最新定位时间
        /// entity_name：entity 唯一标识
        /// entity_desc：entity描述信息
        /// <custom-key>：开发者自定义的 entity 属性字段
        /// </summary>
        public string sortby { get; set; }

        /// <summary>
        /// 排序方向
        /// </summary>
        public bool asc { get; set; } = true;

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">The nv.</param>
        /// <returns></returns>
        public override NameValueCollection FillArgs(NameValueCollection nv)
        {
            nv = base.FillArgs(nv);

            if (string.IsNullOrWhiteSpace(sortby) == false)
            {
                var aa = asc ? "asc" : "desc";
                nv.Add("sortby", $"{sortby.Trim()}:{aa}");
            }

            return nv;
        }
    }

    /// <summary>
    /// 关键字搜索参数
    /// </summary>
    public class EntitySearchParam : EntityListWithOrderParam
    {
        /// <summary>
        /// 搜索关键字,默认为空，检索全部数据支持 entity_name + entity_desc 的联合模糊检索
        /// </summary>
        public string query { get; set; }

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">The nv.</param>
        /// <returns></returns>
        public override NameValueCollection FillArgs(NameValueCollection nv)
        {
            nv = base.FillArgs(nv);

            if (string.IsNullOrWhiteSpace(query) == false)
                nv.Add("query", query);
            return nv;
        }
    }

    /// <summary>
    /// 矩形范围搜索参数
    /// </summary>
    public class EntityBoundSearchParam : EntityListWithOrderParam
    {
        /// <summary>
        /// 左下角
        /// </summary>
        public LocationPoint a { get; set; }

        /// <summary>
        /// 右上角
        /// </summary>
        public LocationPoint b { get; set; }

        /// <summary>
        /// 请求参数 bounds 的坐标类型
        /// </summary>
        public CoordTypeEnums coord_type_input { get; set; } = CoordTypeEnums.bd09ll;

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">The nv.</param>
        /// <returns></returns>
        public override NameValueCollection FillArgs(NameValueCollection nv)
        {
            nv = base.FillArgs(nv);
            nv.Add("coord_type_input", coord_type_input.ToString());
            nv.Add("bounds", $"{Math.Min(a?.latitude ?? 0, b?.latitude ?? 0)},{Math.Min(a?.longitude ?? 0, b?.longitude ?? 0)};{Math.Max(a?.latitude ?? 0, b?.latitude ?? 0)},{Math.Max(a?.longitude ?? 0, b?.longitude ?? 0)}");

            return nv;
        }
    }

    /// <summary>
    /// 周边搜索参数
    /// </summary>
    public class EntityAroundSearchParam : EntityListWithOrderParam
    {
        /// <summary>
        /// 	中心点经纬度,格式为：纬度,经度
        /// </summary>
        public LocationPoint center { get; set; }

        /// <summary>
        /// 右上角,单位：米，取值范围[1,5000]
        /// </summary>
        public int radius { get; set; } = 1000;

        /// <summary>
        /// 请求参数 bounds 的坐标类型
        /// </summary>
        public CoordTypeEnums coord_type_input { get; set; } = CoordTypeEnums.bd09ll;

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">The nv.</param>
        /// <returns></returns>
        public override NameValueCollection FillArgs(NameValueCollection nv)
        {
            nv = base.FillArgs(nv);
            nv.Add("coord_type_input", coord_type_input.ToString());
            nv.Add("center", $"{center?.latitude ?? 0},{center?.longitude ?? 0}");
            if (radius < 1)
                radius = 1;
            else if (radius > 5000)
                radius = 5000;
            nv.Add("radius", radius.ToString());
            return nv;
        }
    }

    /// <summary>
    ///  多边形搜索参数
    /// </summary>
    public class EntityPolygonSearchParam : EntityListWithOrderParam
    {
        /// <summary>
        /// 中心点经纬度,格式为：纬度,经度
        /// 经纬度顺序为：纬度,经度； 顶点顺序可按顺时针或逆时针排列。 多边形外接矩形面积不超过3000平方公里
        /// </summary>
        public List<LocationPoint> vertexes { get; set; }

        /// <summary>
        /// 请求参数 bounds 的坐标类型
        /// </summary>
        public CoordTypeEnums coord_type_input { get; set; } = CoordTypeEnums.bd09ll;

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">The nv.</param>
        /// <returns></returns>
        public override NameValueCollection FillArgs(NameValueCollection nv)
        {
            nv = base.FillArgs(nv);
            nv.Add("coord_type_input", coord_type_input.ToString());
            if (vertexes?.Any() == true)
            {
                nv.Add("vertexes", string.Join(";", vertexes.Select(o => $"{o?.latitude ?? 0},{o?.longitude ?? 0}")));
            }
            return nv;
        }
    }

    /// <summary>
    ///  行政区搜索
    /// </summary>
    public class EntityDistrictSearchParam : EntityListWithOrderParam
    {
        /// <summary>
        /// 行政区划关键字;
        /// 支持中国范围内的国家、省、市、区/县名称。请尽量输入完整的行政区层级和名称，保证名称的唯一性。若输入的行政区名称匹配多个行政区，搜索会失败，将会返回匹配的行政区列表。
        /// 关键字示例： 中国 北京市 湖南省长沙市 湖南省长沙市雨花区
        /// </summary>
        public string keyword { get; set; }

        /// <summary>
        /// 设置返回值的内容
        /// </summary>
        public EntityDistrictSearchReturnTypeEnums return_type { get; set; } = EntityDistrictSearchReturnTypeEnums.all;

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">The nv.</param>
        /// <returns></returns>
        public override NameValueCollection FillArgs(NameValueCollection nv)
        {
            nv = base.FillArgs(nv);
            nv.Add("return_type", return_type.ToString());
            return nv;
        }
    }

    /// <summary>
    /// 行政区搜索返回结果类型
    /// </summary>
    public enum EntityDistrictSearchReturnTypeEnums
    {
        /// <summary>
        /// 仅返回 total，即符合本次检索条件的所有
        /// </summary>
        [Description("")]
        simple,

        /// <summary>
        /// 数量（若仅需行政区内entity数量，建议选择 simple，将提升检索性能）
        /// </summary>
        [Description("entity ")]
        entity,

        /// <summary>
        /// 返回全部结果
        /// </summary>
        [Description("all")]
        all,
    }

    public class EntityDistrictSearchReault : EntityListReault
    {
        /// <summary>
        /// 关键字匹配的行政区划列表
        /// </summary>
        public string[] district_list { get; set; }
    }
}