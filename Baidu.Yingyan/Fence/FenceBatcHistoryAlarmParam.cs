using System;
using System.Collections.Specialized;

namespace Baidu.Yingyan.Fence
{
    /// <summary>
    /// 批量查询所有围栏报警信息
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.Fence.FenceQueryStatusParam" />
    public class FenceBatcHistoryAlarmParam : IYingyanParam
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
        /// 可选，默认值为1。page_index与page_size一起计算从第几条结果返回，代表返回第几页。
        /// </summary>
        public int page_index { get; set; } = 1;

        /// <summary>
        /// 可选，默认值为100。最大值1000。page_size与page_index一起计算从第几条结果返回，代表返回结果中每页有几条记录。
        /// </summary>
        public int page_size { get; set; } = 500;

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
        public virtual NameValueCollection FillArgs(NameValueCollection nv)
        {
            if (nv == null)
                nv = new NameValueCollection();

            if (start_time != null)
                nv["start_time"] = start_time.Value.ToUtcTicks().ToString();
            if (end_time != null)
                nv["end_time"] = end_time.Value.ToUtcTicks().ToString();
            nv["coord_type_output"] = coord_type_output.ToString();
            nv["page_index"] = page_index.ToString();
            nv["page_size"] = page_size.ToString();
            return nv;
        }
    }
}