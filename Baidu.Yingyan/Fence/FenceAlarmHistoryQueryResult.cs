namespace Baidu.Yingyan.Fence
{
    public class FenceAlarmHistoryQueryResult : CommonResult
    {
        /// <summary>
        /// 返回结果的数量
        /// </summary>
        public int size { get; set; }

        /// <summary>
        /// 报警的数量
        /// </summary>
        public FenceAlarmHistory[] alarms { get; set; }
    }
}