using System.Collections.Specialized;

namespace Baidu.Yingyan.Entity
{
    /// <summary>
    /// 周边搜索参数
    /// </summary>
    public class EntityAroundSearchParam : EntityListWithOrderParam
    {
        /// <summary>
        /// 	中心点经纬度,格式为：纬度,经度
        /// </summary>
        public LocationPoint center { get; set; }

        /// <summary>
        /// 右上角,单位：米，取值范围[1,5000]
        /// </summary>
        public int radius { get; set; } = 1000;

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
            nv.Add("center", $"{center?.latitude ?? 0},{center?.longitude ?? 0}");
            if (radius < 1)
                radius = 1;
            else if (radius > 5000)
                radius = 5000;
            nv.Add("radius", radius.ToString());
            return nv;
        }
    }
}