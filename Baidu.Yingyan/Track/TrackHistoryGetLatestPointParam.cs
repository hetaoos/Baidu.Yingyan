using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Baidu.Yingyan.Track
{
    /// <summary>
    /// 实时纠偏参数
    /// </summary>
    public class TrackHistoryGetLatestPointParam : IYingyanParam
    {
        /// <summary>
        /// entity唯一标识
        /// </summary>
        [Required]
        public string entity_name { get; set; }

        /// <summary>
        /// 纠偏选项
        /// </summary>
        public TrackHistoryProcessOption process_option { get; set; }

        /// <summary>
        /// 返回的坐标类型
        /// </summary>
        public CoordTypeEnums coord_type_output { get; set; } = CoordTypeEnums.bd09ll;

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public virtual Dictionary<string, string> FillArgs(Dictionary<string, string> args)
        {
            if (args == null)
                args = new Dictionary<string, string>();
            args["entity_name"] = entity_name;
            var op = process_option?.ToString();
            if (string.IsNullOrWhiteSpace(op) == false)
                args["process_option"] = op;

            args["coord_type_output"] = coord_type_output.ToString();
            return args;
        }
    }
}