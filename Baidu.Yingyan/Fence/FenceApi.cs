using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Baidu.Yingyan.Fence
{
    public class FenceApi
    {
        private YingyanApi framework;
        private Uri url = new Uri(YingyanApi.url + "fence/");

        public FenceApi(YingyanApi framework)
        {
            this.framework = framework;
        }
        /// <summary>
        /// 创建一个新的围栏实体，返回的是本围栏的fence_id，一个地理围栏实体的fence_id加上其所属轨迹服务的service _id，可以用来查找该围栏实体本身的详细信息以及该围栏的状态等。
        /// </summary>
        /// <param name="entity_name">entity唯一标识</param>
        /// <param name="fence">围栏对象</param>
        /// <returns></returns>
        public async Task<FenceCreateResult> create(FenceItemAsArgs fence)
        {
            var args = framework.getArgs(fence.ToDictionary());
            var content = new FormUrlEncodedContent(args);
            return await YingyanApi.post<FenceCreateResult>(url, "create", content, YingyanApi.getDefaultHttpError<FenceCreateResult>());

        }

        /// <summary>
        /// 根据fence_id删除围栏
        /// </summary>
        /// <param name="fence_id">地理围栏的唯一标识</param>
        /// <returns></returns>
        public async Task<CommonResult> delete(int fence_id)
        {
            var args = framework.getArgs();
            args["fence_id"] = fence_id.ToString();
            var content = new FormUrlEncodedContent(args);
            return await YingyanApi.post<CommonResult>(url, "delete", content, YingyanApi.getDefaultHttpError<CommonResult>());

        }

        /// <summary>
        /// 更新一个围栏实体的详细信息。围栏属性信息中各个可选字段如果不填，则不更新相关属性值。
        /// </summary>
        /// <param name="fence">围栏对象</param>
        /// <returns></returns>
        public async Task<CommonResult> update(FenceItemAsArgs fence)
        {
            var args = framework.getArgs(fence.ToDictionary());
            var content = new FormUrlEncodedContent(args);
            return await YingyanApi.post<CommonResult>(url, "update", content, YingyanApi.getDefaultHttpError<CommonResult>());

        }


        /// <summary>
        /// 根据fence_id查询围栏
        /// </summary>
        /// <param name="creator">creator和fence_ids二者至少选一个</param>
        /// <param name="fence_ids">creator和fence_ids二者至少选一个</param>
        /// <returns></returns>
        public async Task<FenceListResult> list(string creator, List<int> fence_ids)
        {
            var nv = framework.getNameValueCollection();
            if (string.IsNullOrEmpty(creator) == false)
                nv.Add("creator", creator);
            if (fence_ids != null && fence_ids.Count > 0)
                nv.Add("fence_ids", string.Join(",", fence_ids.Select(o => o.ToString()).ToArray()));

            return await YingyanApi.get<FenceListResult>(url, "list", nv, YingyanApi.getDefaultHttpError<FenceListResult>());
        }
        /// <summary>
        /// 根据fence_id查询围栏内监控对象是在围栏内还是在围栏外
        /// </summary>
        /// <param name="fence_id">地理围栏的唯一标识</param>
        /// <param name="monitored_persons">围栏监控对象列表,多个对象用逗号分隔。表示查询那些监控对象的状态。不填时，查询所有监控对象的状态</param>
        /// <returns></returns>
        public async Task<FenceStatusResult> querystatus(int fence_id, List<string> monitored_persons)
        {
            var nv = framework.getNameValueCollection();

            nv.Add("fence_id", fence_id.ToString());
            if (monitored_persons != null && monitored_persons.Count > 0)
                nv.Add("monitored_persons", string.Join(",", monitored_persons));

            return await YingyanApi.get<FenceStatusResult>(url, "querystatus", nv, YingyanApi.getDefaultHttpError<FenceStatusResult>());
        }

        /// <summary>
        /// 查询围栏的监控对象的历时报警信息。只提供7天以内（包含7天）的数据查询，7天以外的数据不提供查询服务。
        /// </summary>
        /// <param name="fence_id">地理围栏的唯一标识</param>
        /// <param name="monitored_persons">围栏监控对象列表,多个对象用逗号分隔。表示查询那些监控对象的状态。不填时，查询所有监控对象的状态</param>
        /// <returns></returns>
        public async Task<FenceStatusResult> histroryalarm(int fence_id, List<string> monitored_persons, DateTime? begin_time = null, DateTime? end_time = null)
        {
            var nv = framework.getNameValueCollection();

            nv.Add("fence_id", fence_id.ToString());
            if (monitored_persons != null && monitored_persons.Count > 0)
                nv.Add("monitored_persons", string.Join(",", monitored_persons));
            if (begin_time.HasValue)
                nv.Add("end_time", begin_time.Value.ToUtcTicks().ToString());
            if (begin_time.HasValue)
                nv.Add("end_time", begin_time.Value.ToUtcTicks().ToString());
            return await YingyanApi.get<FenceStatusResult>(url, "histroryalarm", nv, YingyanApi.getDefaultHttpError<FenceStatusResult>());
        }

        /// <summary>
        /// 围栏观察者在设定时间内不再接收报警
        /// </summary>
        /// <param name="fence_id">地理围栏的唯一标识</param>
        /// <param name="observer">围栏观察者</param>
        ///<param name="time">暂停时间,必选。表示在此时间之前不再提醒</param>
        /// <returns></returns>
        public async Task<CommonResult> delayalarm(int fence_id, string observer, DateTime time )
        {
            var args = framework.getArgs();
            args.Add("fence_id", fence_id.ToString());
            args.Add("observer", observer);
            args.Add("time", time.ToUtcTicks().ToString());
            var content = new FormUrlEncodedContent(args);
            return await YingyanApi.post<CommonResult>(url, "delayalarm", content, YingyanApi.getDefaultHttpError<CommonResult>());

        }
    }
}
