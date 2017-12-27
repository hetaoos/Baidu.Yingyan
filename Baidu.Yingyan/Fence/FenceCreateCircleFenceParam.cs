using System.Collections.Specialized;

namespace Baidu.Yingyan.Fence
{
    /// <summary>
    /// 创建圆形围栏
    /// </summary>
    public class FenceCreateCircleFenceParam : FenceBaseData
    {
        /// <summary>
        /// 围栏类型
        /// </summary>
        public override FenceShapeEnums shape => FenceShapeEnums.circle;

        /// <summary>
        /// 围栏圆心
        /// </summary>
        public LocationPoint center { get; set; }

        /// <summary>
        /// 围栏半径
        /// 单位：米，取值范围(0,5000]
        /// </summary>
        public double radius { get; set; }

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">原有参数</param>
        /// <returns>
        /// 填充后的参数
        /// </returns>
        public override NameValueCollection FillArgs(NameValueCollection nv)
        {
            nv = base.FillArgs(nv);
            nv["longitude"] = center?.longitude.ToString();
            nv["latitude"] = center?.latitude.ToString();
            if (radius <= 0)
                radius = 1;
            else if (radius > 5000)
                radius = 5000;
            nv["radius"] = radius.ToString();
            return nv;
        }
    }
}