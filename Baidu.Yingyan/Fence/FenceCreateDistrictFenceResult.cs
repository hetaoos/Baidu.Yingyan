namespace Baidu.Yingyan.Fence
{
    /// <summary>
    /// 创建行政区划围栏结果
    /// </summary>
    public class FenceCreateDistrictFenceResult : FenceCreateFenceResult
    {
        /// <summary>
        /// 结构化的行政区划描述
        /// status=0，围栏创建成功时返回该字段
        /// </summary>
        public string district { get; set; }

        /// <summary>
        /// 关键字匹配的行政区划列表
        /// status=5108：围栏创建失败，关键字匹配至多个行政区时，返回该字段
        /// </summary>
        public string[] district_list { get; set; }
    }
}