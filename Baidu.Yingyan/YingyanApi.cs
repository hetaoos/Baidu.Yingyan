using io.nulldata.Baidu.Yingyan.Entity;
using io.nulldata.Baidu.Yingyan.Track;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace io.nulldata.Baidu.Yingyan
{
    public class YingyanApi
    {
        public string ak { get; private set; }
        public string service_id { get; private set; }

        public EntityApi entity { get; private set; }

        public TrackApi track { get; private set; }

        public const string url = "http://api.map.baidu.com/trace/v2/";

        public YingyanApi(string ak, string service_id)
        {
            this.ak = ak;
            this.service_id = service_id;
            entity = new EntityApi(this);
            track = new TrackApi(this);
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

        internal static Func<HttpResponseMessage, T> getDefaultHttpError<T>()
             where T : CommonResult, new()
        {
            return (o) => new T() { status = (int)o.StatusCode, message = "HTTP 请求异常" };
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
