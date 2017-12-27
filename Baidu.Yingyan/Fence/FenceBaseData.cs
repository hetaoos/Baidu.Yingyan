using System;
using System.Collections.Specialized;

namespace Baidu.Yingyan.Fence
{
    /// <summary>
    /// 围栏信息
    /// </summary>
    public abstract class FenceBaseData : IYingyanParam
    {
        /// <summary>
        /// 围栏类型
        /// </summary>
        public abstract FenceShapeEnums shape { get; }

        /// <summary>
        /// 创建围栏时为空
        /// </summary>
        public int fence_id { get; set; }

        /// <summary>
        /// 围栏名称
        /// </summary>
        public string fence_name { get; set; }

        /// <summary>
        /// 监控对象,支持指定一个entity或针对所有entity设置围栏
        /// 1、对指定一个entity_name创建围栏。
        /// 规则：monitored_person=entity_name
        /// 示例：monitored_person=张三
        /// 如设置为#allentity（monitored_person=#allentity），则对整个service下的所有entity创建围栏
        /// </summary>
        public string monitored_person { get; set; }

        /// <summary>
        /// 坐标类型
        /// </summary>
        public CoordTypeEnums coord_type { get; set; } = CoordTypeEnums.bd09ll;

        /// <summary>
        /// 围栏去噪参数
        /// 单位：米。每个轨迹点都有一个定位误差半径radius，这个值越大，代表定位越不准确，可能是噪点。
        /// 围栏计算时，如果噪点也参与计算，会造成误报的情况。设置denoise可控制，当轨迹点的定位误差半径大于设置值时，就会把该轨迹点当做噪点，不参与围栏计算。
        /// denoise默认值为0，不去噪。
        /// </summary>
        public int denoise { get; set; }

        /// <summary>
        /// 围栏创建时间，返回是有该字段
        /// </summary>
        public DateTime create_time { get; set; }

        /// <summary>
        /// 围栏修改时间，返回是有该字段
        /// </summary>
        public DateTime modify_time { get; set; }

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
            if (fence_id > 0)
                nv["fence_id"] = fence_id.ToString();
            if (string.IsNullOrWhiteSpace(fence_name) == false)
                nv["fence_name"] = fence_name;
            if (string.IsNullOrWhiteSpace(monitored_person))
                monitored_person = "#allentity";
            nv["monitored_person"] = monitored_person;
            nv["coord_type"] = coord_type.ToString();
            if (denoise > 0)
                nv["denoise"] = denoise.ToString();
            return nv;
        }
    }


    /// <summary>
    /// 围栏形状
    /// </summary>
    public enum FenceShapeEnums
    {
        /// <summary>
        /// 圆形
        /// </summary>
        circle,

        /// <summary>
        /// 多边形
        /// </summary>
        polygon,

        /// <summary>
        /// 线型
        /// </summary>
        polyline,

        /// <summary>
        /// 行政区划
        /// </summary>
        district
    }
}