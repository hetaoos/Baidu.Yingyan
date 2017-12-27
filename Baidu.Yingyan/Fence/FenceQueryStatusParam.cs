﻿using System.Collections.Specialized;
using System.Linq;

namespace Baidu.Yingyan.Fence
{
    /// <summary>
    /// 围栏报警查询参数
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.IYingyanParam" />
    public class FenceQueryStatusParam : IYingyanParam
    {
        /// <summary>
        /// 监控对象
        /// </summary>
        public string monitored_person { get; set; }

        /// <summary>
        /// 围栏实体的id列表
        /// </summary>
        public int[] fence_ids { get; set; }

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
            if (fence_ids?.Any() == true)
                nv["fence_ids"] = string.Join(",", fence_ids);
            nv["monitored_person"] = monitored_person;
            return nv;
        }
    }
}