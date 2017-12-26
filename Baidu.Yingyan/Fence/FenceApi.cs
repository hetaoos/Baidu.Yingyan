using System;
using System.Threading.Tasks;

namespace Baidu.Yingyan.Fence
{
    /// <summary>
    /// 地理围栏
    /// </summary>
    public partial class FenceApi
    {
        private YingyanApi framework;
        private Uri url = new Uri(YingyanApi.url + "fence/");

        public FenceApi(YingyanApi framework)
        {
            this.framework = framework;
        }

        /// <summary>
        /// 创建圆形围栏
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<FenceCreateFenceResult> createcirclefence(FenceCreateCircleFenceParam param)
        {
            return framework.post<FenceCreateFenceResult>(url, "createcirclefence", param);
        }

        /// <summary>
        /// 创建多边形围栏
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<FenceCreateFenceResult> createpolygonfence(FenceCreatePolygonFenceParam param)
        {
            return framework.post<FenceCreateFenceResult>(url, "createpolygonfence", param);
        }

        /// <summary>
        /// 创建线型围栏
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<FenceCreateFenceResult> createpolylinefence(FenceCreatePolylineFenceParam param)
        {
            return framework.post<FenceCreateFenceResult>(url, "createpolylinefence", param);
        }

        /// <summary>
        /// 创建行政区划围栏
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<FenceCreateDistrictFenceResult> createdistrictfence(FenceCreateDistrictFenceParam param)
        {
            return framework.post<FenceCreateDistrictFenceResult>(url, "createdistrictfence", param);
        }

        /// <summary>
        /// 更新圆形围栏
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<FenceCreateFenceResult> updatecirclefence(FenceCreateCircleFenceParam param)
        {
            return framework.post<FenceCreateFenceResult>(url, "updatecirclefence", param);
        }

        /// <summary>
        /// 更新多边形围栏
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<FenceCreateFenceResult> updatepolygonfence(FenceCreatePolygonFenceParam param)
        {
            return framework.post<FenceCreateFenceResult>(url, "updatepolygonfence", param);
        }

        /// <summary>
        /// 更新线型围栏
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<FenceCreateFenceResult> updatepolylinefence(FenceCreatePolylineFenceParam param)
        {
            return framework.post<FenceCreateFenceResult>(url, "updatepolylinefence", param);
        }

        /// <summary>
        /// 更新行政区划围栏
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<FenceCreateDistrictFenceResult> updatedistrictfence(FenceCreateDistrictFenceParam param)
        {
            return framework.post<FenceCreateDistrictFenceResult>(url, "updatedistrictfence", param);
        }

        /// <summary>
        /// 删除围栏
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<FenceDeleteFenceResult> delete(FenceDeleteFenceParam param)
        {
            return framework.post<FenceDeleteFenceResult>(url, "delete", param);
        }

        /// <summary>
        /// 查询围栏信息
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<FenceListFenceResult> delete(FenceListFenceParam param)
        {
            return framework.post<FenceListFenceResult>(url, "list", param);
        }
    }
}