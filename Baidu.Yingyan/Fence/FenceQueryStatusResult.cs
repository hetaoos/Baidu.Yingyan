namespace Baidu.Yingyan.Fence
{
    public class FenceQueryStatusResult : CommonResult
    {
        /// <summary>
        /// 返回结果的数量
        /// </summary>
        public int size { get; set; }

        /// <summary>
        /// 报警的数量
        /// </summary>
        public FenceAlarmMonitoredStatus[] monitored_statuses { get; set; }
    }

    /// <summary>
    /// 围栏状态
    /// </summary>
    public class FenceAlarmMonitoredStatus
    {
        /// <summary>
        /// 围栏 id
        /// </summary>
        public string fence_id { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public FenceAlarmMonitoredStatusEnums monitored_status { get; set; }
    }

    /// <summary>
    /// 围栏状态
    /// </summary>
    public enum FenceAlarmMonitoredStatusEnums
    {    /// <summary>
         /// 未知状态
         /// </summary>
        unknown,

        /// <summary>
        /// 在围栏内
        /// </summary>
        @in,

        /// <summary>
        /// 在围栏外
        /// </summary>
        @out,
    }
}