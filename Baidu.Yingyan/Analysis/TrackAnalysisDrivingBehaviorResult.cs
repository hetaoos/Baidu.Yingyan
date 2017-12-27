namespace Baidu.Yingyan.Analysis
{
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

        /// <summary>
        /// 急加速
        /// </summary>
        public TrackAnalysisDrivingBehaviorHarshAccelerationPoint[] harsh_acceleration { get; set; }

        /// <summary>
        /// 急停
        /// </summary>
        public TrackAnalysisDrivingBehaviorHarshAccelerationPoint[] harsh_breaking { get; set; }

        /// <summary>
        /// 急转弯记录
        /// </summary>
        public TrackAnalysisDrivingBehaviorHarshSteeringPoint[] harsh_steering { get; set; }
    }

    /// <summary>
    /// 超速记录点
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.LocationPointWithTime" />
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
    /// 急转弯记录点
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.LocationPointWithTime" />
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

    /// <summary>
    /// 急加速记录点
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.LocationPointWithTime" />
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
}