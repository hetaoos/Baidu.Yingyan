using System.Collections.Generic;

namespace Baidu.Yingyan.Entity
{
    /// <summary>
    /// 周边搜索参数
    /// </summary>
    public class EntityAroundSearchParam : EntityListWithOrderParam
    {
        /// <summary>
        /// 	中心点经纬度,格式为：纬度,经度
        /// </summary>
        public LocationPoint center { get; set; }

        /// <summary>
        /// 右上角,单位：米，取值范围[1,5000]
        /// </summary>
        public int radius { get; set; } = 1000;

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
            args["center"] = $"{center?.latitude ?? 0},{center?.longitude ?? 0}";
            if (radius < 1)
                radius = 1;
            else if (radius > 5000)
                radius = 5000;
            args["radius"] = radius.ToString();
            return args;
        }
    }
}