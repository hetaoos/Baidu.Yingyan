using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Baidu.Yingyan.Entity
{
    /// <summary>
    /// 矩形范围搜索参数
    /// </summary>
    public class EntityBoundSearchParam : EntityListWithOrderParam
    {
        /// <summary>
        /// 左下角
        /// </summary>
        [Required]
        public LocationPoint a { get; set; }

        /// <summary>
        /// 右上角
        /// </summary>
        [Required]
        public LocationPoint b { get; set; }

        /// <summary>
        /// 请求参数 bounds 的坐标类型
        /// </summary>
        public CoordTypeEnums coord_type_input { get; set; } = CoordTypeEnums.bd09ll;

        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public override Dictionary<string, string> FillArgs(Dictionary<string, string> args)
        {
            args = base.FillArgs(args);
            args["coord_type_input"] = coord_type_input.ToString();
            args["bounds"] = $"{Math.Min(a?.latitude ?? 0, b?.latitude ?? 0)},{Math.Min(a?.longitude ?? 0, b?.longitude ?? 0)};{Math.Max(a?.latitude ?? 0, b?.latitude ?? 0)},{Math.Max(a?.longitude ?? 0, b?.longitude ?? 0)}";

            return args;
        }
    }
}