using Baidu.Yingyan.Analysis;
using Baidu.Yingyan.Entity;
using Baidu.Yingyan.Export;
using Baidu.Yingyan.Fence;
using Baidu.Yingyan.Track;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Baidu.Yingyan
{
    /// <summary>
    /// 鹰眼轨迹服务接口
    /// </summary>
    public class YingyanApi
    {
        /// <summary>
        /// 用户的ak
        /// </summary>
        public string ak { get; private set; }

        /// <summary>
        /// service的ID，service 的唯一标识。
        /// </summary>
        public string service_id { get; private set; }

        /// <summary>
        /// 终端管理
        /// </summary>
        public EntityApi entity { get; private set; }

        /// <summary>
        /// 轨迹管理
        /// </summary>
        public TrackApi track { get; private set; }

        /// <summary>
        /// 地理围栏
        /// </summary>
        public FenceApi fence { get; private set; }

        /// <summary>
        /// 轨迹分析
        /// </summary>
        public AnalysisApi analysis { get; private set; }

        /// <summary>
        /// 批量导出
        /// </summary>
        public ExportApi export { get; private set; }

        /// <summary>
        /// 接口地址
        /// </summary>
        public const string url = "http://yingyan.baidu.com/api/v3/";

        private HttpClient client;

        /// <summary>
        /// 鹰眼轨迹服务接口
        /// </summary>
        /// <param name="ak">用户的ak</param>
        /// <param name="service_id">service的ID，service 的唯一标识</param>
        public YingyanApi(string ak, string service_id)
        {
            this.ak = ak;
            this.service_id = service_id;
            client = new HttpClient();
            entity = new EntityApi(this);
            track = new TrackApi(this);
            fence = new FenceApi(this);
            analysis = new AnalysisApi(this);
            export = new ExportApi(this);
            client = new HttpClient();
        }

        /// <summary>
        /// POST 操作
        /// </summary>
        /// <typeparam name="T">返回对象json</typeparam>
        /// <param name="uri">BaseAddress</param>
        /// <param name="requestUri">方法</param>
        /// <param name="content">提交内容</param>
        /// <param name="onError">HTTP 错误时处理</param>
        /// <returns></returns>
        internal static async Task<T> post<T>(Uri uri, string requestUri, HttpContent content, Func<HttpResponseMessage, T> onError = null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsync(requestUri, content);
                if (response.IsSuccessStatusCode)
                    return await response.Content.JsonReadAsAsync<T>();
                if (onError != null)
                    return onError(response);
                return default(T);
            }
        }

        /// <summary>
        /// 获取默认请求错误信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal static Func<HttpResponseMessage, T> getDefaultHttpError<T>()
             where T : CommonResult, new()
        {
            return (o) => new T() { status = StatusCodeEnums.error999, message = "HTTP 请求异常" };
        }

        /// <summary>
        /// GET 操作
        /// </summary>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="uri">基本地址</param>
        /// <param name="requestUri">方法</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        internal Task<TResult> get<TResult>(Uri uri, string requestUri, IYingyanParam param = null)
            where TResult : CommonResult, new()
        {
            var nv = getNameValueCollection();
            if (param != null)
                nv = param.FillArgs(nv);
            return get<TResult>(uri, requestUri, nv, getDefaultHttpError<TResult>());
        }

        /// <summary>
        /// GET 操作
        /// </summary>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="uri">基本地址</param>
        /// <param name="requestUri">方法</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        internal Task<TResult> post<TResult>(Uri uri, string requestUri, IYingyanParam param = null)
            where TResult : CommonResult, new()
        {
            var nv = getNameValueCollection();
            if (param != null)
                nv = param.FillArgs(nv);

            var content = new FormUrlEncodedContent(nv.AllKeys.SelectMany(nv.GetValues, (k, v) => new KeyValuePair<string, string>(k, v)));
            return post<TResult>(uri, requestUri, content, getDefaultHttpError<TResult>());
        }

        /// <summary>
        /// GET 操作
        /// </summary>
        /// <typeparam name="T">返回对象json</typeparam>
        /// <param name="uri">BaseAddress</param>
        /// <param name="requestUri">方法</param>
        /// <param name="content">提交内容</param>
        /// <param name="onError">HTTP 错误时处理</param>
        /// <returns></returns>
        internal static async Task<T> get<T>(Uri uri, string requestUri, NameValueCollection content = null, Func<HttpResponseMessage, T> onError = null)
        {
            if (content != null && content.Count > 0)
            {
                var q = content.ToUriQuery();
                if (string.IsNullOrEmpty(requestUri))
                    requestUri = "?" + q;
                else if (requestUri.IndexOf('?') >= 0)
                {
                    requestUri += "&" + q;
                }
                else
                    requestUri += "?" + q;
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                    return await response.Content.JsonReadAsAsync<T>();
                if (onError != null)
                    return onError(response);
                return default(T);
            }
        }

        /// <summary>
        /// 构造参数
        /// </summary>
        /// <param name="otherValues">The other values.</param>
        /// <returns></returns>
        internal Dictionary<string, string> getArgs(IEnumerable<KeyValuePair<string, string>> otherValues = null)
        {
            var args = new Dictionary<string, string>();
            args["ak"] = ak;
            args["service_id"] = service_id;

            if (otherValues != null && otherValues.Count() > 0)
            {
                foreach (var kv in otherValues)
                    args[kv.Key] = kv.Value;
            }
            return args;
        }

        /// <summary>
        /// 构造参数
        /// </summary>
        /// <param name="otherValues">The other values.</param>
        /// <returns></returns>
        internal NameValueCollection getNameValueCollection(IEnumerable<KeyValuePair<string, string>> otherValues = null)
        {
            var args = new NameValueCollection();
            args["ak"] = ak;
            args["service_id"] = service_id;
            if (otherValues != null && otherValues.Count() > 0)
            {
                foreach (var kv in otherValues)
                    args.Add(kv.Key, kv.Value);
            }
            return args;
        }
    }
}