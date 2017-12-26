using System.Threading.Tasks;

namespace Baidu.Yingyan.Fence
{
    public partial class FenceApi
    {
        /// <summary>
        /// 查询监控对象在围栏内或外
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<FenceQueryStatusResult> querystatus(FenceQueryStatusParam param)
        {
            return framework.get<FenceQueryStatusResult>(url + "querystatus", param);
        }

        /// <summary>
        /// 查询某监控对象的历史报警信息
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<FenceAlarmHistoryQueryResult> historyalarm(FenceHistoryAlarmParam param)
        {
            return framework.get<FenceAlarmHistoryQueryResult>(url + "historyalarm", param);
        }

        /// <summary>
        /// 批量查询某 service 下时间段以内的所有报警信息，用于服务端报警同步
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<FenceBatcHistoryAlarmResult> batchhistoryalarm(FenceBatcHistoryAlarmParam param)
        {
            return framework.get<FenceBatcHistoryAlarmResult>(url + "batchhistoryalarm", param);
        }
    }
}