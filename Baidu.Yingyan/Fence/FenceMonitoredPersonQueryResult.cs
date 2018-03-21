namespace Baidu.Yingyan.Fence
{
    /// <summary>
    /// 查询围栏监控的所有entity结果
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.CommonResult" />
    public class FenceMonitoredPersonQueryResult : CommonResult
    {
        /// <summary>
        /// 查询监控entity的总个数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 本页返回的entity个数
        /// </summary>
        public int size { get; set; }

        /// <summary>
        /// 报警的数量
        /// </summary>
        public string[] monitored_person { get; set; }
    }
}