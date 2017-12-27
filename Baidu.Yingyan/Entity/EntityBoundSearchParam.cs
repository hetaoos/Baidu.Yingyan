using System;
using System.Collections.Specialized;

namespace Baidu.Yingyan.Entity
{
    /// <summary>
    /// 矩形范围搜索参数
    /// </summary>
    public class EntityBoundSearchParam : EntityListWithOrderParam
    {
        /// <summary>
        /// 左下角
        /// </summary>
        public LocationPoint a { get; set; }

        /// <summary>
        /// 右上角
        /// </summary>
        public LocationPoint b { get; set; }

        /// <summary>
        /// 请求参数 bounds 的坐标类型
        /// </summary>
        public CoordTypeEnums coord_type_input { get; set; } = CoordTypeEnums.bd09ll;

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">The nv.</param>
        /// <returns></returns>
        public override NameValueCollection FillArgs(NameValueCollection nv)
        {
            nv = base.FillArgs(nv);
            nv.Add("coord_type_input", coord_type_input.ToString());
            nv.Add("bounds", $"{Math.Min(a?.latitude ?? 0, b?.latitude ?? 0)},{Math.Min(a?.longitude ?? 0, b?.longitude ?? 0)};{Math.Max(a?.latitude ?? 0, b?.latitude ?? 0)},{Math.Max(a?.longitude ?? 0, b?.longitude ?? 0)}");

            return nv;
        }
    }
}