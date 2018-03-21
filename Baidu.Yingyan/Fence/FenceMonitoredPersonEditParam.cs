using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Baidu.Yingyan.Fence
{
    /// <summary>
    /// 增加/删除 围栏需监控的entity
    /// </summary>
    public class FenceMonitoredPersonEditParam : IYingyanParam
    {
        /// <summary>
        /// 围栏的唯一标识
        /// </summary>
        [Required]
        public int fence_id { get; set; }

        /// <summary>
        /// 监控对象
        /// 必选，轨迹服务中的entity_name。
        /// 支持通过entity列表向围栏添加entity。 每次添加entity上限为100个。
        /// 示例：monitored_person =entity_name1, entity_name2, entity_name3 多个entity_name
        /// 使用英文逗号分隔
        /// </summary>
        [Required]
        public string[] monitored_person { get; set; }

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="args">原有参数</param>
        /// <returns>
        /// 填充后的参数
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual Dictionary<string, string> FillArgs(Dictionary<string, string> args)
        {
            if (args == null)
                args = new Dictionary<string, string>();
            args["fence_id"] = fence_id.ToString();
            if (monitored_person?.Any() == true)
            {
                args["monitored_person"] = string.Join(",", monitored_person);
            }
            return args;
        }
    }
}