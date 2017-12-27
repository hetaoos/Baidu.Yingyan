namespace Baidu.Yingyan.Track
{
    /// <summary>
    /// 批量添加的错误点
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.Track.TrackPoint" />
    public class TrackErrorPoint : TrackPoint
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public string error { get; set; }
    }
}