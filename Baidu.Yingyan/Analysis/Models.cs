using Baidu.Yingyan.Converters;
using Baidu.Yingyan.Track;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Baidu.Yingyan.Analysis
{
    /// <summary>
    /// 停留点分析参数
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.Track.TrackHistoryGetLatestPointParam" />
    public class TrackAnalysisStayPointParam : TrackHistoryGetLatestPointParam
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime start_time { get; set; }

        /// <summary>
        /// 结束时间
        /// 结束时间不能大于当前时间，且起止时间区间不超过24小时。为提升响应速度，同时避免轨迹点过多造成请求超时（3s）失败，建议缩短每次请求的时间区间，将一天轨迹拆分成多段进行拼接
        /// </summary>
        public DateTime end_time { get; set; }

        /// <summary>
        /// 停留时间
        /// 单位：秒，默认值：600。该字段用于设置停留点判断规则，即若系统判断在半径为stay_radius的圆形范围内停留时间超过stay_time，则被认为是一次停留
        /// </summary>
        public int stay_time { get; set; } = 600;

        /// <summary>
        /// 停留半径
        /// 单位：米，取值范围：[1,500]，默认值：20。该字段用于设置停留点判断规则，即若系统判断在半径为stay_radius的圆形范围内停留时间超过stay_time，则被认为是一次停留
        /// </summary>
        public int stay_radius { get; set; } = 20;

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">The nv.</param>
        /// <returns></returns>
        public override NameValueCollection FillArgs(NameValueCollection nv)
        {
            nv = base.FillArgs(nv);
            nv.Add("start_time", start_time.ToUtcTicks().ToString());
            nv.Add("end_time", end_time.ToUtcTicks().ToString());
            nv.Add("stay_time", stay_time.ToString());
            if (stay_radius < 1)
                stay_radius = 1;
            else if (stay_radius > 500)
                stay_radius = 500;
            nv.Add("stay_radius", stay_radius.ToString());
            return nv;
        }
    }

    /// <summary>
    /// 停留点分析结果
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.CommonResult" />
    public class TrackAnalysisStayPointResult : CommonResult
    {
        /// <summary>
        /// 停留次数
        /// </summary>
        public int staypoint_num { get; set; }

        /// <summary>
        /// 停留记录列表
        /// </summary>
        public List<TrackAnalysisStayPoint> stay_points { get; set; }
    }

    /// <summary>
    /// 停留点
    /// </summary>
    public class TrackAnalysisStayPoint
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        [JsonConverter(typeof(UnixTicksConverter))]
        public DateTime start_time { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [JsonConverter(typeof(UnixTicksConverter))]
        public DateTime end_time { get; set; }

        /// <summary>
        /// 停留时长，单位：秒
        /// </summary>
        public int duration { get; set; }

        /// <summary>
        /// 停留点
        /// </summary>
        public LocationPoint stay_point { get; set; }
    }

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

    /// <summary>
    /// 驾驶行为分析结果
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.Analysis.TrackAnalysisStayPointParam" />
    public class TrackAnalysisDrivingBehaviorResult : CommonResult
    {
        /// <summary>
        /// 行程里程
        /// </summary>
        public double distance { get; set; }

        /// <summary>
        /// 行程耗时
        /// </summary>
        public int duration { get; set; }

        /// <summary>
        /// 平均时速
        /// </summary>
        public double average_speed { get; set; }

        /// <summary>
        /// 最高时速
        /// </summary>
        public double max_speed { get; set; }

        /// <summary>
        /// 超速次数
        /// </summary>
        public int speeding_num { get; set; }

        /// <summary>
        /// 急加速次数
        /// </summary>
        public int harsh_acceleration_num { get; set; }

        /// <summary>
        /// 急刹车次数
        /// </summary>
        public int harsh_breaking_num { get; set; }

        /// <summary>
        /// 急转弯次数
        /// </summary>
        public int harsh_steering_num { get; set; }

        /// <summary>
        /// 起点信息
        /// </summary>
        public LocationPointWithAddress start_point { get; set; }

        /// <summary>
        /// 终点信息
        /// </summary>
        public LocationPointWithAddress end_point { get; set; }

        /// <summary>
        /// 超速记录集合
        /// </summary>
        public TrackAnalysisDrivingBehaviorSpeedingPoint[][] speeding { get; set; }

        public TrackAnalysisDrivingBehaviorHarshAccelerationPoint[] harsh_acceleration { get; set; }
        public TrackAnalysisDrivingBehaviorHarshAccelerationPoint[] harsh_breaking { get; set; }
        public TrackAnalysisDrivingBehaviorHarshSteeringPoint[] harsh_steering { get; set; }
    }

    public class LocationPointWithAddress : LocationPointWithTime
    {
        /// <summary>
        /// 点地址
        /// </summary>
        public string address { get; set; }
    }

    /// <summary>
    /// 超速记录点
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.Track.LocationPointWithTime" />
    public class TrackAnalysisDrivingBehaviorSpeedingPoint : LocationPointWithTime
    {
        /// <summary>
        /// 实际行驶时速
        /// </summary>
        public double actual_speed { get; set; }

        /// <summary>
        /// 所在道路限定最高时速
        /// </summary>
        public double limit_speed { get; set; }
    }

    /// <summary>
    /// 急加速记录点
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.Track.LocationPointWithTime" />
    public class TrackAnalysisDrivingBehaviorHarshAccelerationPoint : LocationPointWithTime
    {
        /// <summary>
        /// 实际加速度，单位：m/s^2
        /// </summary>
        public double acceleration { get; set; }

        /// <summary>
        /// 加速前时速，单位：km/h
        /// </summary>
        public double initial_speed { get; set; }

        /// <summary>
        /// 加速后时速，单位：km/h
        /// </summary>
        public double end_speed { get; set; }
    }

    /// <summary>
    /// 急转弯记录点
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.Track.LocationPointWithTime" />
    public class TrackAnalysisDrivingBehaviorHarshSteeringPoint : LocationPointWithTime
    {
        /// <summary>
        /// 向心加速度,单位：m/s^2
        /// </summary>
        public double centripetal_acceleration { get; set; }

        /// <summary>
        /// 转向类型
        /// 取值范围：unknow（方向未知）,left（左转）,right（右转）
        /// </summary>
        public string turn_type { get; set; }

        /// <summary>
        /// 转向时速,单位：km/h
        /// </summary>
        public double speed { get; set; }
    }
}