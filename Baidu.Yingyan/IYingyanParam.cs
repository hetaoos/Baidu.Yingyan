using System.Collections.Specialized;

namespace Baidu.Yingyan
{
    /// <summary>
    /// 请求参数
    /// </summary>
    public interface IYingyanParam
    {
        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">原有参数</param>
        /// <returns>填充后的参数</returns>
        NameValueCollection FillArgs(NameValueCollection nv);
    }
}