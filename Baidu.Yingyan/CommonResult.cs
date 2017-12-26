namespace Baidu.Yingyan
{
    /// <summary>
    /// 通用返回结果
    /// </summary>
    public class CommonResult
    {
        /// <summary>
        /// 返回状态，0为成功
        /// </summary>
        public StatusCode status { get; set; }

        /// <summary>
        /// 对status的中文描述
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("status={0}, message={1}", status, message);
        }
    }
}