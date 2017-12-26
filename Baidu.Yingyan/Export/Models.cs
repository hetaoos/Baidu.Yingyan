using Baidu.Yingyan.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Baidu.Yingyan.Export
{
    /// <summary>
    /// 创建任务
    /// </summary>
    public class ExportCreatJobParam : IYingyanParam
    {
        /// <summary>
        /// service的ID，service 的唯一标识。
        /// </summary>
        public int service_id { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime start_time { get; set; }

        /// <summary>
        /// 结束时间
        /// 注：结束时间需比当前最新时间小12小时（即只能下载12小时以前的轨迹），且结束时间和起始时间差在24小时之内（即一次只能下载24小时区间内的轨迹）。
        /// </summary>
        public DateTime end_time { get; set; }

        /// <summary>
        /// 返回的坐标类型
        /// </summary>
        public CoordTypeEnums coord_type_output { get; set; } = CoordTypeEnums.bd09ll;

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">The nv.</param>
        /// <returns></returns>
        public virtual NameValueCollection FillArgs(NameValueCollection nv)
        {
            if (nv == null)
                nv = new NameValueCollection();
            nv.Add("service_id", service_id.ToString());
            nv.Add("start_time", start_time.ToUtcTicks().ToString());
            nv.Add("end_time", end_time.ToUtcTicks().ToString());
            nv.Add("coord_type_output", coord_type_output.ToString());
            return nv;
        }
    }

    /// <summary>
    /// 停留点分析结果
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.CommonResult" />
    public class ExportCreatJobResult : CommonResult
    {
        /// <summary>
        /// 任务id，每个任务的唯一标识
        /// </summary>
        public int job_id { get; set; }
    }

    /// <summary>
    /// 删除任务
    /// </summary>
    public class ExportDeleteJobParam : IYingyanParam
    {
        /// <summary>
        /// service的ID，service 的唯一标识。
        /// </summary>
        public int service_id { get; set; }

        /// <summary>
        /// 任务id
        /// </summary>
        public int job_id { get; set; }

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">The nv.</param>
        /// <returns></returns>
        public virtual NameValueCollection FillArgs(NameValueCollection nv)
        {
            if (nv == null)
                nv = new NameValueCollection();
            nv.Add("service_id", service_id.ToString());
            nv.Add("job_id", job_id.ToString());
            return nv;
        }
    }

    /// <summary>
    /// 查询任务
    /// </summary>
    public class ExportGetJobParam : IYingyanParam
    {
        /// <summary>
        /// service的ID，service 的唯一标识。
        /// </summary>
        public int service_id { get; set; }

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">The nv.</param>
        /// <returns></returns>
        public virtual NameValueCollection FillArgs(NameValueCollection nv)
        {
            if (nv == null)
                nv = new NameValueCollection();
            nv.Add("service_id", service_id.ToString());
            return nv;
        }
    }

    public class ExportGetJobResult : CommonResult
    {
        /// <summary>
        /// 任务总条数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 任务数据
        /// </summary>
        public ExportGetJobData[] jobs { get; set; }
    }

    /// <summary>
    /// 导出任务
    /// </summary>
    public class ExportGetJobData
    {
        /// <summary>
        /// 任务id
        /// </summary>
        public int job_id { get; set; }

        /// <summary>
        /// 	service的ID，service 的唯一标识。
        /// </summary>
        public int service_id { get; set; }

        /// <summary>
        /// 轨迹起始时间
        /// </summary>
        [JsonConverter(typeof(UnixTicksConverter))]
        public DateTime start_time { get; set; }

        /// <summary>
        /// 轨迹结束时间
        /// </summary>
        [JsonConverter(typeof(UnixTicksConverter))]
        public DateTime end_time { get; set; }

        /// <summary>
        /// 返回的坐标类型
        /// </summary>
        public CoordTypeEnums coord_type_output { get; set; }

        /// <summary>
        /// 	任务创建的格式化时间
        /// </summary>
        public DateTime create_time { get; set; }

        /// <summary>
        /// 	任务创建的格式化时间
        /// </summary>
        public DateTime modify_time { get; set; }

        /// <summary>
        /// 任务当前的执行状态
        /// </summary>
        public ExportJobStatusEnums job_status { get; set; }

        /// <summary>
        /// 轨迹数据文件下载链接
        /// 数据准备好后（即：job_status为 done 时），
        /// 将会生成轨迹数据文件下载链接，开发者可通过该链接下载数据文件。注：已完成的任务会在48小时之后自动清理，请及时下载。
        /// </summary>
        public string file_url { get; set; }
    }

    /// <summary>
    /// 任务当前的执行状态
    /// </summary>
    public enum ExportJobStatusEnums
    {
        /// <summary>
        /// 待处理
        /// </summary>
        waiting,

        /// <summary>
        /// 正在准备数据
        /// </summary>
        running,

        /// <summary>
        /// 数据已准备完成，已生成可供下载的数据文件
        /// </summary>
        done
    }

    /// <summary>
    /// 导出的一行数据
    /// </summary>
    public class ExportData
    {
        /// <summary>
        /// 鹰眼内部使用的id，可以忽略
        /// </summary>
        public int entity_id { get; set; }

        /// <summary>
        /// entity名称，其唯一标识
        /// </summary>
        public string entity_name { get; set; }

        /// <summary>
        /// entity 可读性描述
        /// </summary>
        public string entity_desc { get; set; }

        /// <summary>
        /// 最新的轨迹点信息
        /// </summary>
        public ExportDataLocationPoint loc { get; set; }

        /// <summary>
        /// 该entity最新定位时间
        /// </summary>
        [JsonConverter(typeof(UnixTicksConverter))]
        public DateTime loc_time { get; set; }

        /// <summary>
        /// entity属性修改时间，该时间为服务端时间
        /// </summary>
        [JsonConverter(typeof(UnixTicksConverter))]
        public DateTime modify_time { get; set; }

        /// <summary>
        /// entity创建时间，该时间为服务端时间
        /// </summary>
        [JsonConverter(typeof(UnixTicksConverter))]
        public DateTime create_time { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>
        public ExportDataCustomData custom_data { get; set; }
    }

    /// <summary>
    /// 最新的轨迹点信息
    /// </summary>
    public class ExportDataLocationPoint : LocationPoint
    {
        /// <summary>
        /// 坐标系类型，等效于CoordType2Enums
        /// </summary>
        public CoordType2Enums coord_type { get; set; }
    }

    /// <summary>
    /// 扩展数据
    /// </summary>
    public class ExportDataCustomData
    {
        /// <summary>
        /// 定位精度(m)
        /// </summary>
        public double? radius { get; set; }

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
}