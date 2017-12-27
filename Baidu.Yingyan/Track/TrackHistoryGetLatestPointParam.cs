using System.Collections.Specialized;

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
        /// <param name="nv">The nv.</param>
        /// <returns></returns>
        public virtual NameValueCollection FillArgs(NameValueCollection nv)
        {
            if (nv == null)
                nv = new NameValueCollection();
            nv.Add("entity_name", entity_name);
            var op = process_option?.ToString();
            if (string.IsNullOrWhiteSpace(op) == false)
                nv.Add("process_option", op);

            nv.Add("coord_type_output", coord_type_output.ToString());
            return nv;
        }
    }
}