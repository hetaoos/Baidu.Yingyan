using Baidu.Yingyan.Analysis;
using Baidu.Yingyan.Entity;
using Baidu.Yingyan.Export;
using Baidu.Yingyan.Fence;
using Baidu.Yingyan.Track;
using System;
using System.Collections.Generic;
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
        /// sn签名的验证方式的 Security Key
        /// </summary>
        public string sk { get; private set; }

        /// <summary>
        /// 终端管理/实时位置搜索
        /// </summary>
        public EntityApi entity { get; private set; }

        /// <summary>
        /// 轨迹上传/轨迹查询和纠偏
        /// </summary>
        public TrackApi track { get; private set; }

        /// <summary>
        /// 轨迹分析
        /// </summary>
        public AnalysisApi analysis { get; private set; }

        /// <summary>
        /// 地理围栏管理/报警
        /// </summary>
        public FenceApi fence { get; private set; }

        /// <summary>
        /// 批量导出轨迹
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
        /// <param name="sk">sn签名的验证方式的 Security Key</param>
        public YingyanApi(string ak, string service_id, string sk = null)
        {
            this.ak = ak;
            this.service_id = service_id;
            this.sk = sk;
            client = new HttpClient();
            entity = new EntityApi(this);
            track = new TrackApi(this);
            fence = new FenceApi(this);
            analysis = new AnalysisApi(this);
            export = new ExportApi(this);
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// GET 操作
        /// </summary>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="requestUri">方法</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        internal Task<TResult> get<TResult>(string requestUri, Dictionary<string, string> param)
            where TResult : CommonResult, new()
        {
            return get<TResult>(requestUri, new DictionaryYingyanParam(param));
        }

        /// <summary>
        /// GET 操作
        /// </summary>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="requestUri">方法</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        internal async Task<TResult> get<TResult>(string requestUri, IYingyanParam param = null)
        where TResult : CommonResult, new()
        {
            var args = getDefaultArgs();
            if (param != null)
                args = param.FillArgs(args);

            calcSN(requestUri, args, false);

            if (args?.Count > 0)
            {
                var q = args.ToUriQuery();
                if (string.IsNullOrEmpty(requestUri))
                    requestUri = "?" + q;
                else if (requestUri.IndexOf('?') >= 0)
                {
                    requestUri += "&" + q;
                }
                else
                    requestUri += "?" + q;
            }

            var response = await client.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
                return await response.Content.JsonReadAsAsync<TResult>();

            var r = new TResult();
            r.status = StatusCodeEnums.error999;
            r.message = $"http 请求错误：StatusCode={response.StatusCode}, ReasonPhrase={response.ReasonPhrase}";
            return r;
        }

        /// <summary>
        /// GET 操作
        /// </summary>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="requestUri">方法</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        internal Task<TResult> post<TResult>(string requestUri, Dictionary<string, string> param)
            where TResult : CommonResult, new()
        {
            return post<TResult>(requestUri, new DictionaryYingyanParam(param));
        }

        /// <summary>
        /// GET 操作
        /// </summary>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="requestUri">方法</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        internal async Task<TResult> post<TResult>(string requestUri, IYingyanParam param = null)
        where TResult : CommonResult, new()
        {
            var args = getDefaultArgs();
            if (param != null)
                args = param.FillArgs(args);

            calcSN(requestUri, args, true);

            var content = new FormUrlEncodedContent(args);

            var response = await client.PostAsync(requestUri, content);
            if (response.IsSuccessStatusCode)
                return await response.Content.JsonReadAsAsync<TResult>();
            var r = new TResult();
            r.status = StatusCodeEnums.error999;
            r.message = $"http 请求错误：StatusCode={response.StatusCode}, ReasonPhrase={response.ReasonPhrase}";
            return r;
        }

        /// <summary>
        /// 构造参数
        /// </summary>
        /// <param name="otherValues">The other values.</param>
        /// <returns></returns>
        internal Dictionary<string, string> getDefaultArgs(Dictionary<string, string> otherValues = null)
        {
            var args = new Dictionary<string, string>()
            {
                ["ak"] = ak,
                ["service_id"] = service_id
            };

            if (otherValues?.Any() == true)
            {
                foreach (var kv in otherValues)
                    args[kv.Key] = kv.Value;
            }
            return args;
        }

        /// <summary>
        /// 计算sn
        /// </summary>
        /// <param name="requestUri">特殊的请求地址</param>
        /// <param name="args">请求参数</param>
        /// <param name="post">是Post或者是Get方式</param>
        private void calcSN(string requestUri, IDictionary<string, string> args, bool post = false)
        {
            if (string.IsNullOrWhiteSpace(sk))
                return;

            var uri = new Uri(new Uri(url), requestUri);
            var requestPath = uri.AbsolutePath;

            //POST计算sn时，参数要排序
            var dic = post ? new SortedDictionary<string, string>(args) : args;

            var sn = BaiduSNCaculater.CaculateSN(sk, requestPath, dic);
            args["sn"] = sn;
        }
    }
}