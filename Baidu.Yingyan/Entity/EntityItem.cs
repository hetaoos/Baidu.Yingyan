using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Baidu.Yingyan.Entity
{
    /// <summary>
    /// entity 对象
    /// </summary>
    public class EntityItem
    {
        /// <summary>
        /// entity名称，其唯一标识
        /// </summary>
        public string entity_name { get; set; }

        /// <summary>
        /// entity 可读性描述
        /// </summary>
        public string entity_desc { get; set; }

        /// <summary>
        /// entity属性修改时间，该时间为服务端时间
        /// </summary>
        public DateTime modify_time { get; set; }

        /// <summary>
        /// entity创建时间，该时间为服务端时间
        /// </summary>
        public DateTime create_time { get; set; }

        /// <summary>
        /// 最新的轨迹点信息
        /// </summary>
        public EntityLocationPoint latest_location { get; set; }

        /// <summary>
        /// 开发者自定义的entity属性信息
        /// </summary>
        [JsonExtensionData]
        public IDictionary<string, object> columns { get; set; }
    }
}