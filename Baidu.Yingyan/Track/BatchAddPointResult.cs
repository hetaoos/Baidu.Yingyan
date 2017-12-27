namespace Baidu.Yingyan.Track
{
    /// <summary>
    /// 批量添加点返回结果
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.CommonResult" />
    public class BatchAddPointResult : CommonResult
    {
        /// <summary>
        /// 上传成功的点个数
        /// </summary>
        public int success_num { get; set; }

        /// <summary>
        /// 上传失败的点信息
        /// </summary>
        public BatchAddPointFailInfo fail_info { get; set; }
    }

    /// <summary>
    /// 上传失败的点信息
    /// </summary>
    public class BatchAddPointFailInfo
    {
        /// <summary>
        /// 输入参数不正确导致的上传失败的点
        /// </summary>
        public TrackErrorPoint[] param_error { get; set; }

        /// <summary>
        /// 服务器内部错误导致上传失败的点
        /// </summary>
        public TrackErrorPoint[] internal_error { get; set; }
    }
}