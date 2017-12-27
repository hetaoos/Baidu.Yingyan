using System.Collections.Specialized;

namespace Baidu.Yingyan.Analysis
{
    /// <summary>
    /// 驾驶行为分析
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.Analysis.TrackAnalysisStayPointParam" />
    public class TrackAnalysisDrivingBehaviorParam : TrackAnalysisStayPointParam
    {
        /// <summary>
        /// 固定限速值
        /// 0：根据百度地图道路限速数据计算超速点 其他数值：以设置的数值为阈值，轨迹点速度超过该值则认为是超速；
        /// </summary>
        public double? speeding_threshold { get; set; }

        /// <summary>
        /// 急加速的加速度阈值
        /// 默认值：1.67，单位：m^2/s，仅支持正数
        /// </summary>
        public double? harsh_acceleration_threshold { get; set; }

        /// <summary>
        /// 急减速的加速度阈值
        /// 默认值：-1.67，单位：m^2/s，仅支持负数
        /// </summary>
        public double? harsh_breaking_threshold { get; set; }

        /// <summary>
        /// 急转弯的向心加速度阈值
        /// 	默认值：5，单位：m^2/s，仅支持正数
        /// </summary>
        public double? harsh_steering_threshold { get; set; }

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">The nv.</param>
        /// <returns></returns>
        public override NameValueCollection FillArgs(NameValueCollection nv)
        {
            nv = base.FillArgs(nv);
            if (speeding_threshold > 0)
                nv.Add("speeding_threshold", speeding_threshold.ToString());
            if (harsh_acceleration_threshold > 0)
                nv.Add("harsh_acceleration_threshold", harsh_acceleration_threshold.ToString());
            if (harsh_breaking_threshold < 0)
                nv.Add("harsh_breaking_threshold", harsh_breaking_threshold.ToString());
            if (harsh_steering_threshold > 0)
                nv.Add("harsh_steering_threshold", harsh_steering_threshold.ToString());
            return nv;
        }
    }
}