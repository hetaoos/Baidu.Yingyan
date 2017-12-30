using System.Collections.Generic;

namespace Baidu.Yingyan.Export
{
    /// <summary>
    /// 删除任务
    /// </summary>
    public class ExportDeleteJobParam : IYingyanParam
    {
        /// <summary>
        /// 任务id
        /// </summary>
        public int job_id { get; set; }

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public virtual Dictionary<string, string> FillArgs(Dictionary<string, string> args)
        {
            if (args == null)
                args = new Dictionary<string, string>();
            args["job_id"] = job_id.ToString();
            return args;
        }
    }
}