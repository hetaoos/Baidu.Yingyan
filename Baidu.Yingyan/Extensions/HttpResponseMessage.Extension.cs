using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Baidu.Yingyan
{
    /// <summary>
    /// HttpResponseMessageExtension
    /// </summary>
    public static class HttpResponseMessageExtension
    {
        /// <summary>
        /// 读取内容流中的数据并转换为json对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static async Task<T> JsonReadAsAsync<T>(this HttpContent content)
        {
            var json = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}