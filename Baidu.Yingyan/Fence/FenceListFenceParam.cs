using System.Collections.Specialized;

namespace Baidu.Yingyan.Fence
{
    /// <summary>
    /// 查询围栏参数
    /// </summary>
    public class FenceListFenceParam : FenceDeleteFenceParam
    {
        /// <summary>
        /// 输出坐标类型
        /// </summary>
        public CoordTypeEnums coord_type_output { get; set; } = CoordTypeEnums.bd09ll;

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">原有参数</param>
        /// <returns>
        /// 填充后的参数
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override NameValueCollection FillArgs(NameValueCollection nv)
        {
            nv = base.FillArgs(nv);
            nv["coord_type_output"] = coord_type_output.ToString();
            return nv;
        }
    }
}