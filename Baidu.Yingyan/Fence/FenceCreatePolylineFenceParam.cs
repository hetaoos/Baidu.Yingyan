using System.Collections.Generic;
using System.Linq;

namespace Baidu.Yingyan.Fence
{
    /// <summary>
    /// 创建线型围栏
    /// </summary>
    public class FenceCreatePolylineFenceParam : FenceBaseData
    {
        /// <summary>
        /// 围栏类型
        /// </summary>
        public override FenceShapeEnums shape => FenceShapeEnums.polyline;

        /// <summary>
        /// 线型围栏坐标点
        /// 经纬度顺序为：纬度,经度；
        /// 坐标点个数在2-100个之间
        /// </summary>
        public LocationPoint[] vertexes { get; set; }

        /// <summary>
        /// 偏离距离
        /// 偏移距离（若偏离折线距离超过该距离即报警），单位：米   示例：200
        /// </summary>
        public int offset { get; set; }

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="args">原有参数</param>
        /// <returns>
        /// 填充后的参数
        /// </returns>
        public override Dictionary<string, string> FillArgs(Dictionary<string, string> args)
        {
            args = base.FillArgs(args);
            if (vertexes?.Any() == true)
            {
                var vs = string.Join(";", vertexes.Select(o => $"{o?.latitude},{o?.longitude}"));
                args["vertexes"] = vs;
            }
            args["offset"] = offset.ToString();
            return args;
        }
    }
}