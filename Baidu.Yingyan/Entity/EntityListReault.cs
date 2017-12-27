namespace Baidu.Yingyan.Entity
{
    /// <summary>
    /// entity 搜索类接口通用返回结果
    /// </summary>
    /// <seealso cref="Baidu.Yingyan.CommonResult" />
    public class EntityListReault : CommonResult
    {
        /// <summary>
        /// 本页返回的结果条数
        /// </summary>
        public int size { get; set; }

        /// <summary>
        /// 本次检索总结果条数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// entity详细信息列表
        /// </summary>
        public EntityItem[] entities { get; set; }
    }
}