using System.Threading.Tasks;

namespace Baidu.Yingyan.Fence
{
    /// <summary>
    /// 地理围栏
    /// 围栏管理类接口主要负责围栏的创建、更新、删除和查询，支持以下类型的围栏
    /// 地理围栏报警类接口主要提供
    /// 1. 查询监控对象在围栏内/外：查询被监控对象在指定围栏内或外，也支持查询被监控对象目前相对于所有围栏的状态
    /// 2. 查询围栏报警信息：支持查询某时间段内单个围栏或该 service 下所有围栏的报警信息
    /// 3. 服务端报警信息推送：鹰眼将报警信息实时推送至开发者的服务端
    /// <a href="http://lbsyun.baidu.com/index.php?title=yingyan/api/v3/geofence">围栏管理类</a>
    /// <a href="http://lbsyun.baidu.com/index.php?title=yingyan/api/v3/geofencealarm">地理围栏报警</a>
    /// </summary>
    public partial class FenceApi
    {
        private YingyanApi framework;
        private const string url = "fence/";

        /// <summary>
        /// Initializes a new instance of the <see cref="FenceApi"/> class.
        /// </summary>
        /// <param name="framework">The framework.</param>
        public FenceApi(YingyanApi framework)
        {
            this.framework = framework;
        }

        #region 围栏管理类

        /// <summary>
        /// 创建圆形围栏
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<FenceCreateFenceResult> createcirclefence(FenceCreateCircleFenceParam param)
        {
            return framework.post<FenceCreateFenceResult>(url + "createcirclefence", param);
        }

        /// <summary>
        /// 创建多边形围栏
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<FenceCreateFenceResult> createpolygonfence(FenceCreatePolygonFenceParam param)
        {
            return framework.post<FenceCreateFenceResult>(url + "createpolygonfence", param);
        }

        /// <summary>
        /// 创建线型围栏
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<FenceCreateFenceResult> createpolylinefence(FenceCreatePolylineFenceParam param)
        {
            return framework.post<FenceCreateFenceResult>(url + "createpolylinefence", param);
        }

        /// <summary>
        /// 创建行政区划围栏
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<FenceCreateDistrictFenceResult> createdistrictfence(FenceCreateDistrictFenceParam param)
        {
            return framework.post<FenceCreateDistrictFenceResult>(url + "createdistrictfence", param);
        }

        /// <summary>
        /// 更新圆形围栏
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<FenceCreateFenceResult> updatecirclefence(FenceCreateCircleFenceParam param)
        {
            return framework.post<FenceCreateFenceResult>(url + "updatecirclefence", param);
        }

        /// <summary>
        /// 更新多边形围栏
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<FenceCreateFenceResult> updatepolygonfence(FenceCreatePolygonFenceParam param)
        {
            return framework.post<FenceCreateFenceResult>(url + "updatepolygonfence", param);
        }

        /// <summary>
        /// 更新线型围栏
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<FenceCreateFenceResult> updatepolylinefence(FenceCreatePolylineFenceParam param)
        {
            return framework.post<FenceCreateFenceResult>(url + "updatepolylinefence", param);
        }

        /// <summary>
        /// 更新行政区划围栏
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<FenceCreateDistrictFenceResult> updatedistrictfence(FenceCreateDistrictFenceParam param)
        {
            return framework.post<FenceCreateDistrictFenceResult>(url + "updatedistrictfence", param);
        }

        /// <summary>
        /// 删除围栏
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<FenceDeleteFenceResult> delete(FenceDeleteFenceParam param)
        {
            return framework.post<FenceDeleteFenceResult>(url + "delete", param);
        }

        /// <summary>
        /// 查询围栏信息
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<FenceListFenceResult> delete(FenceListFenceParam param)
        {
            return framework.post<FenceListFenceResult>(url + "list", param);
        }

        #endregion 围栏管理类

        #region 地理围栏报警

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

        #endregion 地理围栏报警
    }
}