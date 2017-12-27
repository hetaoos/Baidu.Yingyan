using Baidu.Yingyan.Converters;
using Newtonsoft.Json;
using System;

namespace Baidu.Yingyan.Export
{
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
}