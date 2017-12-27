using Baidu.Yingyan.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Baidu.Yingyan.Entity
{
    /// <summary>
    /// 最新的轨迹点信息
    /// </summary>
    public class EntityLocationPoint : LocationPoint
    {
        /// <summary>
        /// 定位精度(m)
        /// </summary>
        public double? radius { get; set; }

        /// <summary>
        /// 该entity最新定位时间
        /// </summary>
        [JsonConverter(typeof(UnixTicksConverter))]
        public DateTime loc_time { get; set; }

        /// <summary>
        /// 方向,范围为[0,359]，0度为正北方向，顺时针
        /// </summary>
        public int? direction { get; set; }

        /// <summary>
        /// 速度,(km/h)
        /// </summary>
        public double? speed { get; set; }

        /// <summary>
        /// 高度,(m)
        /// </summary>
        public double? height { get; set; }

        /// <summary>
        /// 楼层,若处于百度支持室内定位的区域，则将返回楼层信息，默认 null
        /// </summary>
        public string floor { get; set; }

        /// <summary>
        /// 距中心点距离，单位：m，仅在周边搜索（entity/aroundsearch）时返回该字段
        /// </summary>
        public double? distance { get; set; }

        /// <summary>
        /// 对象数据名称
        /// </summary>
        public string object_name { get; set; }

        /// <summary>
        /// 开发者自定义track的属性，只有当开发者为track创建了自定义属性字段，且赋过值，才会返回
        /// </summary>
        [JsonExtensionData]
        public IDictionary<string, object> columns { get; set; }
    }
}