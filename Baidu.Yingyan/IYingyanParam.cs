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

    /// <summary>
    /// NameValueCollection 请求参数
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.IYingyanParam" />
    public class NameValueCollectionYingyanParam : IYingyanParam
    {
        /// <summary>
        /// 值列表
        /// </summary>
        public NameValueCollection values { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NameValueCollectionYingyanParam"/> class.
        /// </summary>
        public NameValueCollectionYingyanParam()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NameValueCollectionYingyanParam"/> class.
        /// </summary>
        /// <param name="values">The values.</param>
        public NameValueCollectionYingyanParam(NameValueCollection values)
        {
            this.values = values;
        }

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="nv">原有参数</param>
        /// <returns>
        /// 填充后的参数
        /// </returns>
        public NameValueCollection FillArgs(NameValueCollection nv)
        {
            if (nv == null)
                nv = new NameValueCollection();
            if (values?.Count > 0)
                nv.Add(values);
            return nv;
        }

        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <param name="item">对象</param>

        public static implicit operator NameValueCollectionYingyanParam(NameValueCollection item)
        {
            return new NameValueCollectionYingyanParam(item);
        }
    }
}