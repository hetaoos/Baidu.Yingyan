namespace Baidu.Yingyan.Fence
{
    /// <summary>
    /// 批量查询所有围栏报警信息结果
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.Fence.FenceAlarmHistoryQueryResult" />
    public class FenceBatchHistoryAlarmResult : FenceAlarmHistoryQueryResult
    {
        /// <summary>
        /// 符合条件的总报警数
        /// </summary>
        public int total { get; set; }
    }
}