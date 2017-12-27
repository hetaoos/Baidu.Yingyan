namespace Baidu.Yingyan.Export
{
    /// <summary>
    /// 停留点分析结果
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.CommonResult" />
    public class ExportCreatJobResult : CommonResult
    {
        /// <summary>
        /// 任务id，每个任务的唯一标识
        /// </summary>
        public int job_id { get; set; }
    }
}