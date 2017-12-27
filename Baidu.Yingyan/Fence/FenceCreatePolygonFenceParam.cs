﻿using System.Collections.Specialized;
using System.Linq;

namespace Baidu.Yingyan.Fence
{
    /// <summary>
    /// 创建多边形围栏
    /// </summary>
    public class FenceCreatePolygonFenceParam : FenceBaseData
    {
        /// <summary>
        /// 围栏类型
        /// </summary>
        public override FenceShapeEnums shape => FenceShapeEnums.polygon;

        /// <summary>
        /// 多边形围栏形状点,
        /// 经纬度顺序为：纬度,经度；
        /// 顶点顺序可按顺时针或逆时针排列；
        /// 顶点个数在3-100个之间
        /// </summary>
        public LocationPoint[] vertexes { get; set; }

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
            if (vertexes?.Any() == true)
            {
                var vs = string.Join(";", vertexes.Select(o => $"{o?.latitude},{o?.longitude}"));
                nv["vertexes"] = vs;
            }
            return nv;
        }
    }
}