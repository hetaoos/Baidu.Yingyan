using System.Collections.Specialized;

namespace Baidu.Yingyan.Export
{
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
}