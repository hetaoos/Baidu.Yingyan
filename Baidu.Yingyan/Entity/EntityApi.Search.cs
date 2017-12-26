using System.Threading.Tasks;

namespace Baidu.Yingyan.Entity
{
    /// <summary>
    /// 终端管理类接口主要实现：entity的创建、更新、删除、查询。例如：添加一辆车、删除一辆车、更新车辆的属性信息（如：车辆所属运营区）等。
    /// <a href="http://lbsyun.baidu.com/index.php?title=yingyan/api/v3/entity">终端管理</a>
    /// </summary>
    public partial class EntityApi
    {
        /// <summary>
        /// 根据关键字搜索entity
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<EntityListReault> search(EntitySearchParam param)
        {
            return framework.get<EntityListReault>(url, "search", param);
        }

        /// <summary>
        /// 矩形范围搜索
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<EntityListReault> boundsearch(EntityBoundSearchParam param)
        {
            return framework.get<EntityListReault>(url, "boundsearch", param);
        }

        /// <summary>
        /// 周边搜索
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<EntityListReault> aroundsearch(EntityAroundSearchParam param)
        {
            return framework.get<EntityListReault>(url, "aroundsearch", param);
        }

        /// <summary>
        /// 多边形搜索
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<EntityListReault> polygonsearch(EntityPolygonSearchParam param)
        {
            return framework.get<EntityListReault>(url, "polygonsearch", param);
        }

        /// <summary>
        /// 行政区搜索
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<EntityDistrictSearchReault> districtsearch(EntityDistrictSearchParam param)
        {
            return framework.get<EntityDistrictSearchReault>(url, "districtsearch", param);
        }
    }
}