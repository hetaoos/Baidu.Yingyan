using System.Collections.Generic;
using System.Threading.Tasks;

namespace Baidu.Yingyan.Entity
{
    /// <summary>
    /// 终端管理类接口主要实现：entity的创建、更新、删除、查询。例如：添加一辆车、删除一辆车、更新车辆的属性信息（如：车辆所属运营区）等。
    /// <a href="http://lbsyun.baidu.com/index.php?title=yingyan/api/v3/entity">终端管理</a>
    /// </summary>
    public partial class EntityApi
    {
        private YingyanApi framework;
        private const string url = "entity/";

        public EntityApi(YingyanApi framework)
        {
            this.framework = framework;
        }

        /// <summary>
        /// 添加一个新的entity，一个entity可以是一个人、一辆车、或者任何运动的物体。
        /// </summary>
        /// <param name="entity_name">entity名称，作为其唯一标识。128字节。同一service服务中entity_name不可重复。一旦创建，entity_name 不可更新。</param>
        /// <param name="entity_desc">entity 的可读性描述，128字节，</param>
        /// <param name="columns">开发者自定义字段(可选)</param>
        /// <returns></returns>
        public Task<CommonResult> add(string entity_name, string entity_desc = null, Dictionary<string, string> columns = null)
        {
            var args = framework.getNameValueCollection(columns);
            args["entity_name"] = entity_name;
            args["entity_desc"] = entity_desc;

            return framework.post<CommonResult>(url + "add", args);
        }

        /// <summary>
        /// 更新 entity 的属性信息，如 entity 的描述、entity自定义属性字段的值。
        /// </summary>
        /// <param name="entity_name">entity名称，作为其唯一标识。</param>
        /// <param name="entity_desc">entity 可读性描述</param>
        /// <param name="columns">开发者自定义字段(可选)</param>
        /// <returns></returns>
        public Task<CommonResult> update(string entity_name, string entity_desc = null, Dictionary<string, string> columns = null)
        {
            var args = framework.getNameValueCollection(columns);
            args["entity_name"] = entity_name;
            args["entity_desc"] = entity_desc;
            return framework.post<CommonResult>(url + "update", args);
        }

        /// <summary>
        /// 根据entity_name删除一个entity。
        /// </summary>
        /// <param name="entity_name">entity名称，作为其唯一标识。</param>
        /// <returns></returns>
        public Task<CommonResult> delete(string entity_name)
        {
            var args = framework.getNameValueCollection();
            args["entity_name"] = entity_name;

            return framework.post<CommonResult>(url + "delete", args);
        }

        /// <summary>
        /// 查询entity：根据service_id、entity_name和自定义检索字段，查询本service中所有符合条件的entity信息及其实时位置。
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <returns></returns>
        public Task<EntityListReault> list(EntityListParam param = null)
        {
            return framework.get<EntityListReault>(url + "list", param);
        }
    }
}