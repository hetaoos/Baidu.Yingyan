using System;
using System.Collections.Specialized;

namespace Baidu.Yingyan.Fence
{
    /// <summary>
    /// 查询某监控对象的围栏报警信息
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.Fence.FenceQueryStatusParam" />
    public class FenceHistoryAlarmParam : FenceQueryStatusParam
    {
        /// <summary>
        /// 开始时间,
        /// 若不填，则返回7天内所有报警信息
        /// </summary>
        public DateTime? start_time { get; set; }

        /// <summary>
        /// 结束时间
        /// 若不填，则返回7天内所有报警信息
        /// </summary>
        public DateTime? end_time { get; set; }

        /// <summary>
        /// 返回坐标类型
        /// </summary>
        public CoordTypeEnums coord_type_output { get; set; } = CoordTypeEnums.bd09ll;

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">原有参数</param>
        /// <returns>
        /// 填充后的参数
        /// </returns>
        public override NameValueCollection FillArgs(NameValueCollection nv)
        {
            nv = base.FillArgs(nv);
            if (start_time != null)
                nv["start_time"] = start_time.Value.ToUtcTicks().ToString();
            if (end_time != null)
                nv["end_time"] = end_time.Value.ToUtcTicks().ToString();
            nv["coord_type_output"] = coord_type_output.ToString();
            return nv;
        }
    }
}