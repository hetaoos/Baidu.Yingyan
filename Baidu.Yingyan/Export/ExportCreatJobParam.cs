using System;
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
}